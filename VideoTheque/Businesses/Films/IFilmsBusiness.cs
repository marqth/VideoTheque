using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FilmDto>> GetFilms();
        Task<FilmDto?> GetFilmById(int id);
        Task<FilmDto> InsertFilm(FilmDto film);
        Task UpdateFilm(int id, FilmDto film);
        Task DeleteFilm(int id);
    }
}