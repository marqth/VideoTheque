using VideoTheque.DTOs;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.AgeRatings;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IFilmsRepository _filmsRepository;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IGenresRepository _genreRepository;
        private readonly IAgeRatingsRepository _ageRatingRepository;

        public FilmsBusiness(IFilmsRepository filmsRepository, IPersonnesRepository personnesRepository, IGenresRepository genreRepository, IAgeRatingsRepository ageRatingRepository)
        {
            _filmsRepository = filmsRepository;
            _personnesRepository = personnesRepository;
            _genreRepository = genreRepository;
            _ageRatingRepository = ageRatingRepository;
        }

        public async Task<List<FilmDto>> GetFilms()
        {
            var films = await _filmsRepository.GetFilms();
            return films;
        }

        public async Task<FilmDto?> GetFilmById(int id)
        {
            var film = await _filmsRepository.GetFilmById(id);
            return film;
        }

        public async Task<FilmDto> InsertFilm(FilmDto filmDto)
        {
            await _filmsRepository.InsertFilm(filmDto);
            return filmDto;
        }

        public async Task UpdateFilm(int id, FilmDto filmDto)
        {
            await _filmsRepository.UpdateFilm(id, filmDto);
        }

        public async Task DeleteFilm(int id)
        {
            await _filmsRepository.DeleteFilm(id);
        }
    }
}