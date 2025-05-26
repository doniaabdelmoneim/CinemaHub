using CinemaHub.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaHub.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display (Name = "Profile Picture")]
        [Required (ErrorMessage = "Profile Picture is required")]
        public string PhotoUrl { get; set; }

        [Required]
        [StringLength (100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength (100)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        [DataType (DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [StringLength (500)]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        // relationships
        public List<ActorMovies> ActorMovies { get; set; }


    }
}
