using CinemaHub.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace CinemaHub.Models
{
    public class Producer: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display (Name = "Logo")]
        public string CompanyLogoUrl { get; set; }
        [Required]
        [StringLength (100)]
        public string Name { get; set; }
        [StringLength (500)]
        public string Bio { get; set; }
        [Display (Name = "Company")]
   
        public string ContactEmail { get; set; }

        // relationships
        public List<Movie> Movies{ get; set; } 

    }
}
