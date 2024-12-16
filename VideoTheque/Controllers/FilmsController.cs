namespace VideoTheque.Controllers
{
    using Mapster;
    using Microsoft.AspNetCore.Mvc;
    using VideoTheque.Businesses.Films;
    using VideoTheque.DTOs;
    using VideoTheque.ViewModels;

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
            
            TypeAdapterConfig<FilmDto, FilmViewModel>.NewConfig()
                .Map(dest => dest.MainActor, src => _filmsBusiness.GetPersonNameById(src.IdFirstActor).Result)
                .Map(dest => dest.Director, src => _filmsBusiness.GetPersonNameById(src.IdDirector).Result)
                .Map(dest => dest.Scenarist, src => _filmsBusiness.GetPersonNameById(src.IdScenarist).Result)
                .Map(dest => dest.AgeRating, src => _filmsBusiness.GetAgeRatingNameById(src.IdAgeRating).Result)
                .Map(dest => dest.Genre, src => _filmsBusiness.GetGenreNameById(src.IdGenre).Result)
                .Map(dest => dest.Support, src => "BluRay");

            TypeAdapterConfig<FilmViewModel, FilmDto>.NewConfig()
                .Map(dest => dest.IdFirstActor, src => _filmsBusiness.GetPersonIdByName(src.MainActor).Result)
                .Map(dest => dest.IdDirector, src => _filmsBusiness.GetPersonIdByName(src.Director).Result)
                .Map(dest => dest.IdScenarist, src => _filmsBusiness.GetPersonIdByName(src.Scenarist).Result)
                .Map(dest => dest.IdAgeRating, src => _filmsBusiness.GetAgeRatingIdByName(src.AgeRating).Result)
                .Map(dest => dest.IdGenre, src => _filmsBusiness.GetGenreIdByName(src.Genre).Result);
        }

        [HttpGet]
        public async Task<List<FilmViewModel>> GetFilms() => (await _filmsBusiness.GetFilms()).Adapt<List<FilmViewModel>>();

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetFilm([FromRoute] int id) => (await _filmsBusiness.GetFilmById(id)).Adapt<FilmViewModel>();

        [HttpPost]
        public async Task<IResult> InsertFilm([FromBody] FilmViewModel filmVM)
        {
            var created = await _filmsBusiness.InsertFilm(filmVM.Adapt<FilmDto>());
            return Results.Created($"/films/{created.Id}", created.Adapt<FilmViewModel>());
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