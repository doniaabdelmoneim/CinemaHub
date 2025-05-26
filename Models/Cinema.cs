using CinemaHub.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace CinemaHub.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength (100)]
        public string Name { get; set; }
        [StringLength (500)]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Display (Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; }
        [Display (Name = "Opening Hours")]
        public string OpeningHours { get; set; }
        [Display (Name = "Total Halls")]
        public int TotalHalls { get; set; }

        [Display (Name = "Cinema Image")]
        public string CinemaImageUrl { get; set; }
        // relationships
        public List<Movie> movies { get; set; }
    }
}
