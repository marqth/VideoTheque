// File: VideoTheque/Controllers/EmpruntsController.cs
namespace VideoTheque.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VideoTheque.ViewModels;
    using VideoTheque.DTOs;
    using Mapster;
    using VideoTheque.Businesses.Emprunts;
    
    [ApiController]
    [Route("emprunt")]
    public class EmpruntsController : ControllerBase
    {
        private readonly IEmpruntsBusiness _empruntsBusiness;

        public EmpruntsController(IEmpruntsBusiness empruntsBusiness)
        {
            _empruntsBusiness = empruntsBusiness;
        }

        [HttpGet("{IdFilmPartenaire}")]
        public async Task<FilmPartenaireViewModel> GetFilm(int IdFilmPartenaire) =>
            (await _empruntsBusiness.GetFilmById(IdFilmPartenaire)).Adapt<FilmPartenaireViewModel>();

        [HttpGet("dispo")]
        public async Task<List<FilmViewModel>> GetAvailableFilms() =>
            (await _empruntsBusiness.GetAvailableFilms()).Adapt<List<FilmViewModel>>();
        
        [HttpPost("{IdFilmPartenaire}")]
        public async Task<IActionResult> CreateEmprunt(int IdFilmPartenaire)
        {
            var success = await _empruntsBusiness.CreateEmprunt(IdFilmPartenaire);
            if (!success) return BadRequest("Film is not available or does not exist.");
            return StatusCode(201);
        }
        
        [HttpDelete("{TitreFilm}")]
        public async Task<IActionResult> CancelEmprunt(string TitreFilm)
        {
            var success = await _empruntsBusiness.CancelEmprunt(TitreFilm);
            if (!success) return BadRequest("Film is not found or already available.");
            return StatusCode(201);
        }
    }
}