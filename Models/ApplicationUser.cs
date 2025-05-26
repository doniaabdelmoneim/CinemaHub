using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CinemaHub.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name ="full name")]
        public string FullName { get; set; } 
    }
}
