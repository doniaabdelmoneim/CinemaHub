
using System.ComponentModel.DataAnnotations;

namespace CinemaHub.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email is Required")]
        public string EmailAddress { get; set; }

        [Required (ErrorMessage = "password is Required")]
        [DataType (DataType.Password)]

        public string Password { get; set; }
    }
}
