// File: `VideoTheque/Businesses/Emprunt/IEmpruntsBusiness.cs`
namespace VideoTheque.Businesses.Emprunt
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VideoTheque.DTOs;

    public interface IEmpruntsBusiness
    {
        Task<FilmDto> GetFilmById(int id);
        Task<List<FilmDto>> GetAvailableFilms();
        Task<bool> CreateEmprunt(int idFilmPartenaire);
        Task<bool> CancelEmprunt(string titreFilm);
        Task<List<FilmDispoDto>> GetAvailableFilmsByHost(int idHost);
        Task CreateEmpruntForHost(int idHost, int idFilmPartenaire); // New method
    }
}