using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class FilmViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titre")]
        [Required]
        public string Title { get; set; }

        [JsonPropertyName("duree")]
        [Required]
        public int Duration { get; set; }

        [JsonPropertyName("acteur-principal")]
        [Required]
        public string MainActor { get; set; }

        [JsonPropertyName("realisateur")]
        [Required]
        public string Director { get; set; }

        [JsonPropertyName("scenariste")]
        [Required]
        public string Scenarist { get; set; }

        [JsonPropertyName("age-rating")]
        [Required]
        public string AgeRating { get; set; }

        [JsonPropertyName("genre")]
        [Required]
        public string Genre { get; set; }

        [JsonPropertyName("support")]
        [Required]
        public string Support { get; set; }

        public FrontFilmDTO ToDto()
        {
            return new FrontFilmDTO
            {
                Id = this.Id,
                Title = this.Title,
                Duration = this.Duration,
                MainActor = this.MainActor,
                Director = this.Director,
                Scenarist = this.Scenarist,
                AgeRating = this.AgeRating,
                Genre = this.Genre,
                Support = this.Support
            };
        }

        public static FilmViewModel ToModel(FrontFilmDTO dto)
        {
            return new FilmViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Duration = dto.Duration,
                MainActor = dto.MainActor,
                Director = dto.Director,
                Scenarist = dto.Scenarist,
                AgeRating = dto.AgeRating,
                Genre = dto.Genre,
                Support = dto.Support
            };
        }
    }
}