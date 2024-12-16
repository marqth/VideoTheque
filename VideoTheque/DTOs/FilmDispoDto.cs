// File: `VideoTheque/DTOs/FilmDispoDto.cs`
namespace VideoTheque.DTOs
{
    public class FilmDispoDto
    {
        public int FilmId { get; set; }
        public string Titre { get; set; }
        public string Genre { get; set; }
        public string ActeurPrincipal { get; set; }
        public string Realisateur { get; set; }
    }
}