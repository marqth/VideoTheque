// File: `VideoTheque/Repositories/Films/IFilmsRepository.cs`
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Films
{
    public interface IFilmsRepository
    {
        Task<List<BluRayDto>> GetFilms();
        Task<BluRayDto?> GetFilmById(int id);
        Task<List<BluRayDto?>> GetAvailableFilms();
        Task<BluRayDto?> GetFilmByTitle(string title); // New method
        Task InsertFilm(BluRayDto bluRayDto);
        Task UpdateFilm(int id, BluRayDto bluRayDto);
        Task DeleteFilm(int id);
    }
}