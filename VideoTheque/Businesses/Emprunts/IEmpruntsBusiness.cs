// File: `VideoTheque/Businesses/Emprunt/IEmpruntsBusiness.cs`
namespace VideoTheque.Businesses.Emprunts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VideoTheque.DTOs;

    public interface IEmpruntsBusiness
    {
        Task<FilmDto> GetFilmById(int id);
        Task<List<FilmDispoDto>> GetAvailableFilms();
        Task<bool> CreateEmprunt(int idFilmPartenaire);
        Task<bool> CancelEmprunt(string titreFilm);
    }
}