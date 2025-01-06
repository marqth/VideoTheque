using System.Text.Json.Serialization;

namespace VideoTheque.ViewModels
{
    public class FilmPartenaireDispoViewModel
    {
        [JsonPropertyName("filmId")]
        public int FilmId { get; set; }

        [JsonPropertyName("titre")]
        public string Titre { get; set; }

        [JsonPropertyName("genre")]
        public string Genre { get; set; }

        [JsonPropertyName("acteur-principal")]
        public string ActeurPrincipal { get; set; }

        [JsonPropertyName("realisateur")]
        public string Realisateur { get; set; }
    }
}