using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CinemaHub.Data.Enums;
using CinemaHub.Data.Base;

namespace CinemaHub.Models
{
    public class Movie: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Title is required")]
        [StringLength (100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Description is required")]
        [StringLength (1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string Description { get; set; }

        [Display (Name = "Duration (minutes)")]
        [Range (1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
        public int DurationMinutes { get; set; }
        [Display (Name = "Release Date")]
        [DataType (DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display (Name = "Poster URL")]
        [Url (ErrorMessage = "Please enter a valid URL")]
        public string PosterUrl { get; set; }

        [Display (Name = "Director")]
        [StringLength (100, ErrorMessage = "Director name cannot be longer than 100 characters")]
        public string? Director { get; set; } = "N/A";

        [Display (Name = "Language")]
        [StringLength (50, ErrorMessage = "Language cannot be longer than 50 characters")]
        public string Language { get; set; } = "English";

        [Display (Name = "Age Rating")]
        [StringLength (10, ErrorMessage = "Rating cannot be longer than 10 characters")]
        public string? AgeRating { get; set; } = "PG-13";

        [Display (Name = "IMDB Rating")]
        [Range (0, 10, ErrorMessage = "IMDB rating must be between 0 and 10")]
        public double? ImdbRating { get; set; }=6.5;

        public double price { get; set; }

        [Display (Name = "Is Active")]
        public bool IsActive { get; set; } = true;


        public MovieCategory Category { get; set; } 

        [Display (Name = "Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        // relationships
        public List<ActorMovies> ActorMovies { get; set; }
        //cinema (1 to many relationShip) 
        public int CinemaId { get; set; }
        [ForeignKey ("CinemaId")]
        public Cinema Cinema { get; set; }
        //producer (1 to many relationShip)
        public int ProducerId { get; set; }
        [ForeignKey ("ProducerId")]
        public Producer Producer { get; set; }

    }
}
