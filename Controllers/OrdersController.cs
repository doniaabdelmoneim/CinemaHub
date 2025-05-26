using CinemaHub.Data.Cart;
using CinemaHub.Data.Services;
using CinemaHub.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaHub.Controllers
{
[Authorize]

    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _orderService;

        public OrdersController (IMovieService movieService, ShoppingCart shoppingCart, IOrdersService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }
        // GET: Orders
        public async Task <IActionResult> Index ()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders =await  _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View (orders);
        }
        public IActionResult ShoppingCart()
        {
            {
                var items = _shoppingCart.GetshoppingCartItems ();
                _shoppingCart.ShoppingCartItems = items;
                var response = new ShoppingCartVM ()
                {
                    ShoppingCart = _shoppingCart,
                    ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()

                };
                return View (response);
            }
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item=await _movieService.GetMovieByIdAsync(id);
            if(item != null){
                _shoppingCart.AddItemToCart (item);
            }
            return RedirectToAction ("ShoppingCart");

        }

        public async Task<IActionResult>RemoveItemFromShoppingCart (int id)
        {
            var item =await _movieService.GetMovieByIdAsync (id);
            if (item != null)
            {
                _shoppingCart.RemoveItemToCart (item);
            }
            return RedirectToAction ("ShoppingCart");
        }
        public async Task <IActionResult> CompleteOrder  ()
        {
            var items = _shoppingCart.GetshoppingCartItems ();
            string userId = User.FindFirstValue (ClaimTypes.NameIdentifier); ;
            string userEmailAddress = User.FindFirstValue (ClaimTypes.Email); ;
            _orderService.StoreOrderAsync (items, userId, userEmailAddress).Wait ();
            await _shoppingCart.ClearShoppingCartAsync();
            return View ("OrderCompleted"); 

        }

    }
}

