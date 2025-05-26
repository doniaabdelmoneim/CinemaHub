﻿using CinemaHub.Data;
using CinemaHub.Data.Static;
using CinemaHub.Data.ViewModels;
using CinemaHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CinemaHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>
            signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task <IActionResult> Users()
        {
            var users=await _context.Users.ToListAsync();
            return View(users);

        }
     
        public IActionResult Login ()
        {
            var response = new LoginVM ();
            return View (response);
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View (loginVM);
            }
            var user = await _userManager.FindByEmailAsync (loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync (user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync (user, loginVM.Password,false,false  );
                    if (result.Succeeded)
                    {
                        return RedirectToAction ("Index", "Home");
                    }
                }
                TempData["Error"] = "Invalid Login Attempt";
                return View (loginVM); 
            }
             TempData["Error"] = "Invalid Login Attempt";
            return View (loginVM);
        }

        public IActionResult Register ()
        {
            var response = new RegisterVM ();
            return View (response);
        }
       [HttpPost] 
       public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View ("Register", registerVM);
            }
            var user = await _userManager.FindByEmailAsync (registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Email already exists";
                return View (registerVM);
            }

            var newUser = new ApplicationUser ()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress 
            };
            var newUserResponce = await _userManager.CreateAsync (newUser, registerVM.Password);
            if (newUserResponce.Succeeded)
            {
                await _userManager.AddToRoleAsync (newUser,UserRoles.User);
            }
            return View ("RegisterCompleted");

        }

        //LogOut
       [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync ();
            return RedirectToAction ("Index", "Home");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View ();

        }




    }
}
