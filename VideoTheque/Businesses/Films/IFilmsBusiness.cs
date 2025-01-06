using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FilmDto>> GetFilms();
        Task<FilmDto?> GetFilmById(int id);
        Task InsertFilm(FilmDto film);
        Task UpdateFilm(int id, FilmDto film);
        Task DeleteFilm(int id);
        Task<string> GetPersonNameById(int id);
        Task<int> GetPersonIdByName(string fullname);
        Task<string> GetAgeRatingNameById(int id);
        Task<int> GetAgeRatingIdByName(string name);
        Task<string> GetGenreNameById(int id);
        Task<int> GetGenreIdByName(string name);
        Task<List<FilmDispoDto>> GetAvailableFilmsByHost(int idHost);
        Task CreateEmpruntForHost(int idHost, int idFilmPartenaire); 
    }
}