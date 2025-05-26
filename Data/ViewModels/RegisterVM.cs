using System.ComponentModel.DataAnnotations;

namespace CinemaHub.Data.ViewModels
{
    public  class RegisterVM
    {
      
            [Display (Name = "Full Name")]
            [Required (ErrorMessage = "Full Name is Required")]
            public string FullName { get; set; }

            [Display (Name = "Email Address")]
            [Required (ErrorMessage = "Email is Required")]
            public string EmailAddress { get; set; }
           
            [Required (ErrorMessage = "Password is Required")]
            [DataType (DataType.Password)]
            public string Password { get; set; }
            
            [DataType (DataType.Password)]
            [Display (Name = "Confirm Password")]
            [Compare ("Password", ErrorMessage = "Password and Confirm Password do not match")]
            public string ConfirmPassword { get; set; }
        
    }
}
