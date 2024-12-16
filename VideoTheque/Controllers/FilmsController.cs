// File: VideoTheque/Controllers/FilmsController.cs
namespace VideoTheque.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VideoTheque.Businesses.Films;
    using VideoTheque.DTOs;
    using VideoTheque.ViewModels;
    using Mapster;

    [ApiController]
    [Route("films")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmsBusiness _filmsBusiness;
        protected readonly ILogger<FilmsController> _logger;

        public FilmsController(ILogger<FilmsController> logger, IFilmsBusiness filmsBusiness)
        {
            _logger = logger;
            _filmsBusiness = filmsBusiness;
        }

        [HttpGet]
        public async Task<List<FilmViewModel>> GetFilms() => (await _filmsBusiness.GetFilms()).Adapt<List<FilmViewModel>>();

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetFilm([FromRoute] int id) => (await _filmsBusiness.GetFilmById(id)).Adapt<FilmViewModel>();

        [HttpPost]
        public async Task<IResult> InsertFilm([FromBody] FilmViewModel filmVM)
        {
            await _filmsBusiness.InsertFilm(filmVM.Adapt<FilmDto>());
            return Results.Created($"/films/{filmVM.Id}", filmVM);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateFilm([FromRoute] int id, [FromBody] FilmViewModel filmVM)
        {
            await _filmsBusiness.UpdateFilm(id, filmVM.Adapt<FilmDto>());
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteFilm([FromRoute] int id)
        {
            await _filmsBusiness.DeleteFilm(id);
            return Results.Ok();
        }
    }
}