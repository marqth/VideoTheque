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

            // Mapster configuration
            TypeAdapterConfig<FilmDto, FilmViewModel>.NewConfig()
                .Map(dest => dest.MainActor, src => GetPersonNameById(src.IdFirstActor))
                .Map(dest => dest.Director, src => GetPersonNameById(src.IdDirector))
                .Map(dest => dest.Scenarist, src => GetPersonNameById(src.IdScenarist))
                .Map(dest => dest.AgeRating, src => GetAgeRatingNameById(src.IdAgeRating))
                .Map(dest => dest.Genre, src => GetGenreNameById(src.IdGenre));

            TypeAdapterConfig<FilmViewModel, FilmDto>.NewConfig()
                .Map(dest => dest.IdFirstActor, src => GetPersonIdByName(src.MainActor))
                .Map(dest => dest.IdDirector, src => GetPersonIdByName(src.Director))
                .Map(dest => dest.IdScenarist, src => GetPersonIdByName(src.Scenarist))
                .Map(dest => dest.IdAgeRating, src => GetAgeRatingIdByName(src.AgeRating))
                .Map(dest => dest.IdGenre, src => GetGenreIdByName(src.Genre));
        }

        private string GetPersonNameById(int id)
        {
            // Implement logic to get person name by id
            return "Person Name";
        }

        private int GetPersonIdByName(string name)
        {
            // Implement logic to get person id by name
            return 1;
        }

        private string GetAgeRatingNameById(int id)
        {
            // Implement logic to get age rating name by id
            return "Age Rating Name";
        }

        private int GetAgeRatingIdByName(string name)
        {
            // Implement logic to get age rating id by name
            return 1;
        }

        private string GetGenreNameById(int id)
        {
            // Implement logic to get genre name by id
            return "Genre Name";
        }

        private int GetGenreIdByName(string name)
        {
            // Implement logic to get genre id by name
            return 1;
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