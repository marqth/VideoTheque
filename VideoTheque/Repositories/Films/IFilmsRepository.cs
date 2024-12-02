using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Films
{
    public interface IFilmsRepository
    {
        Task<List<FilmDto>> GetFilms();
        Task<FilmDto?> GetFilmById(int id);
        Task InsertFilm(FilmDto film);
        Task UpdateFilm(int id, FilmDto film);
        Task DeleteFilm(int id);
    }
}