// File: `VideoTheque/DTOs/FilmDispoDto.cs`

using System.Text.Json.Serialization;

namespace VideoTheque.DTOs
{
    public class FilmDispoDto
    {
        public int FilmId { get; set; }
        public string Titre { get; set; }
        public string Genre { get; set; }
        [JsonPropertyName("acteur-principal")]
        public string ActeurPrincipal { get; set; }
        public string Realisateur { get; set; }
    }
}