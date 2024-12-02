using VideoTheque.DTOs;
using VideoTheque.Repositories.Films;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IFilmsRepository _filmsRepository;

        public FilmsBusiness(IFilmsRepository filmsRepository)
        {
            _filmsRepository = filmsRepository;
        }

        public async Task<List<FilmDto>> GetFilms()
        {
            return await _filmsRepository.GetFilms();
        }

        public async Task<FilmDto?> GetFilmById(int id)
        {
            return await _filmsRepository.GetFilmById(id);
        }

        public async Task<FilmDto> InsertFilm(FilmDto film) // Changed return type to FilmDto
        {
            await _filmsRepository.InsertFilm(film);
            return film; // Return the inserted film
        }

        public async Task UpdateFilm(int id, FilmDto film)
        {
            await _filmsRepository.UpdateFilm(id, film);
        }

        public async Task DeleteFilm(int id)
        {
            await _filmsRepository.DeleteFilm(id);
        }
    }
}