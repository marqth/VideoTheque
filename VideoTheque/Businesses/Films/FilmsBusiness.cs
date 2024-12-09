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
            return await _filmsRepository.GetFilms();
        }

        public async Task<FilmDto?> GetFilmById(int id)
        {
            return await _filmsRepository.GetFilmById(id);
        }

        public async Task<FilmDto> InsertFilm(FrontFilmDTO frontFilm)
        {
            // Split the names into LastName and FirstName
            var mainActorNames = frontFilm.MainActor.Split(' ');
            var directorNames = frontFilm.Director.Split(' ');
            var scenaristNames = frontFilm.Scenarist.Split(' ');

            // Retrieve the IDs of related entities
            var mainActor = await _personnesRepository.GetPersonneByLastNameAndFirstName(mainActorNames[0], mainActorNames[1]);
            var director = await _personnesRepository.GetPersonneByLastNameAndFirstName(directorNames[0], directorNames[1]);
            var scenarist = await _personnesRepository.GetPersonneByLastNameAndFirstName(scenaristNames[0], scenaristNames[1]);
            var ageRating = await _ageRatingRepository.GetAgeRatingByName(frontFilm.AgeRating);
            var genre = await _genreRepository.GetGenreByName(frontFilm.Genre);

            if (mainActor == null || director == null || scenarist == null || ageRating == null || genre == null)
            {
                throw new KeyNotFoundException("One or more related entities not found");
            }

            // Create the FilmDto
            var film = new FilmDto
            {
                Title = frontFilm.Title,
                Duration = frontFilm.Duration,
                IdFirstActor = mainActor.Id,
                IdDirector = director.Id,
                IdScenarist = scenarist.Id,
                IdAgeRating = ageRating.Id,
                IdGenre = genre.Id,
                IsAvailable = true,
                IdOwner = null
            };

            // Insert the FilmDto
            await _filmsRepository.InsertFilm(film);
            return film;
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