using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FrontFilmDTO?>> GetFilms();
        Task<FrontFilmDTO?> GetFilmById(int id);
        Task<FilmDto> InsertFilm(FrontFilmDTO film); // Changed return type to FilmDto
        Task UpdateFilm(int id, FrontFilmDTO film);
        Task DeleteFilm(int id);
    }
}