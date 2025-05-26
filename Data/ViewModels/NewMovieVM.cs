using CinemaHub.Data.Enums;
using CinemaHub.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaHub.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is requried")]
        [Display(Description="movie name")]
         public string Title { get; set; }
        [Required (ErrorMessage = "Description is required")]
        [Display (Description = "movie Description")]
        public string Description { get; set; }
        [Display (Description = "price in $")]
        [Range (1, 300, ErrorMessage = "price must be between 1 and 300 $")]
        public double price { get; set; }
        [Display (Description = "Start Date")]
        [Required (ErrorMessage = "start date required")]
        public DateTime startDate { get; set; }
        [Display (Description = "End Date")]
        [Required (ErrorMessage = "End  date required")]
        public DateTime EndDate { get; set; }
        [Display (Description = "select actor(s)")]
        [Required (ErrorMessage = "movie actor(s) is required ")]
        public List<int> ActorsIds { get; set; }
        [Display (Description = "select category")]
        [Required (ErrorMessage = "movie category is required ")]
        public MovieCategory Category { get; set; }

        public string PosterUrl { get; set; }

        [Display (Description = "select a cinema")]
        [Required (ErrorMessage = "movie cinema is required ")]
        public int CinemaId { get; set; }

        [Display (Description = "select a producer")]
        [Required (ErrorMessage = "movie producer is required ")]
        public int ProducerId { get; set; }
    }
}
