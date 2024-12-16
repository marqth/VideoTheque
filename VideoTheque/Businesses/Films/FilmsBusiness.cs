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

        public async Task<List<FilmDto>> GetFilms() {
            var films = await _filmsRepository.GetFilms();

            var filmDtos = await Task.WhenAll(films.Select(async film => new FilmDto
            {
                Id = film.Id,
                Title = film.Title,
                Duration = film.Duration,
                FirstActor = await _personnesRepository.GetPersonne(film.IdFirstActor),
                Director = await _personnesRepository.GetPersonne(film.IdDirector),
                Scenarist = await _personnesRepository.GetPersonne(film.IdScenarist),
                AgeRating = await _ageRatingRepository.GetAgeRating(film.IdAgeRating),
                Genre = await _genreRepository.GetGenre(film.IdGenre),
                IsAvailable = film.IsAvailable,
                IdOwner = film.IdOwner
            }));

            return filmDtos.ToList();
        }

        public async Task<FilmDto?> GetFilmById(int id)
        {
            var film = await _filmsRepository.GetFilmById(id);
            if (film == null) return null;

            var firstActor = await _personnesRepository.GetPersonne(film.IdFirstActor);
            var director = await _personnesRepository.GetPersonne(film.IdDirector);
            var scenarist = await _personnesRepository.GetPersonne(film.IdScenarist);
            var ageRating = await _ageRatingRepository.GetAgeRating(film.IdAgeRating);
            var genre = await _genreRepository.GetGenre(film.IdGenre);

            return new FilmDto
            {
                Id = film.Id,
                Title = film.Title,
                Duration = film.Duration,
                FirstActor = firstActor,
                Director = director,
                Scenarist = scenarist,
                AgeRating = ageRating,
                Genre = genre,
                IsAvailable = film.IsAvailable,
                IdOwner = film.IdOwner
            };
        }

        public async Task InsertFilm(FilmDto filmDto)
        
        
        
        
        {
            var bluRayDto = new BluRayDto
            {
                Id = filmDto.Id,
                Title = filmDto.Title,
                Duration = filmDto.Duration,
                IdFirstActor = filmDto.FirstActor.Id,
                IdDirector = filmDto.Director.Id,
                IdScenarist = filmDto.Scenarist.Id,
                IdAgeRating = filmDto.AgeRating.Id,
                IdGenre = filmDto.Genre.Id,
                IsAvailable = filmDto.IsAvailable,
                IdOwner = filmDto.IdOwner
            };

            await _filmsRepository.InsertFilm(bluRayDto);
        }

        public async Task UpdateFilm(int id, FilmDto filmDto)
        {
            var bluRayDto = new BluRayDto
            {
                Id = filmDto.Id,
                Title = filmDto.Title,
                Duration = filmDto.Duration,
                IdFirstActor = filmDto.FirstActor.Id,
                IdDirector = filmDto.Director.Id,
                IdScenarist = filmDto.Scenarist.Id,
                IdAgeRating = filmDto.AgeRating.Id,
                IdGenre = filmDto.Genre.Id,
                IsAvailable = filmDto.IsAvailable,
                IdOwner = filmDto.IdOwner
            };

            await _filmsRepository.UpdateFilm(id, bluRayDto);
        }

        public async Task DeleteFilm(int id)
        {
            await _filmsRepository.DeleteFilm(id);
        }

        public async Task<string> GetPersonNameById(int id)
        {
            var person = await _personnesRepository.GetPersonne(id);
            return person?.FullName ?? "Unknown Person";
        }

        public async Task<int> GetPersonIdByName(string fullName)
        {
            var names = fullName.Split(' ');
            if (names.Length < 2)
            {
                return 0; // or handle the error as needed
            }
            var firstName = names[0];
            var lastName = names[1];
            var person = await _personnesRepository.GetPersonneByLastNameAndFirstName(lastName, firstName);
            System.Console.WriteLine(person);
            return person?.Id ?? 0;
        }

        public async Task<string> GetAgeRatingNameById(int id)
        {
            var ageRating = await _ageRatingRepository.GetAgeRating(id);
            return ageRating?.Name ?? "Unknown Age Rating";
        }

        public async Task<int> GetAgeRatingIdByName(string name)
        {
            var ageRating = await _ageRatingRepository.GetAgeRatingByName(name);
            return ageRating?.Id ?? 0;
        }

        public async Task<string> GetGenreNameById(int id)
        {
            var genre = await _genreRepository.GetGenre(id);
            return genre?.Name ?? "Unknown Genre";
        }

        public async Task<int> GetGenreIdByName(string name)
        {
            var genre = await _genreRepository.GetGenreByName(name);
            return genre?.Id ?? 0;
        }
    }
}