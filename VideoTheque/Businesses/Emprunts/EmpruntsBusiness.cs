// File: VideoTheque/Businesses/Emprunt/EmpruntsBusiness.cs
namespace VideoTheque.Businesses.Emprunt
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VideoTheque.DTOs;
    using VideoTheque.Repositories.Films;
    using VideoTheque.Repositories.Personnes;
    using VideoTheque.Repositories.Genres;
    using VideoTheque.Repositories.AgeRatings;
    using System.Net.Http.Json;
    using VideoTheque.ViewModels;
    using Mapster;

    public class EmpruntsBusiness : IEmpruntsBusiness
    {
        private readonly IFilmsRepository _filmsRepository;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IGenresRepository _genreRepository;
        private readonly IAgeRatingsRepository _ageRatingRepository;
        private readonly HttpClient _httpClient;

        public EmpruntsBusiness(IFilmsRepository filmsRepository, IPersonnesRepository personnesRepository, IGenresRepository genreRepository, IAgeRatingsRepository ageRatingRepository,  HttpClient httpClient)
        {
            _filmsRepository = filmsRepository;
            _personnesRepository = personnesRepository;
            _genreRepository = genreRepository;
            _ageRatingRepository = ageRatingRepository;
            _httpClient = httpClient;
            
        }
        
        public async Task<List<FilmDto>> GetAvailableFilms()
        {
            var films = await _filmsRepository.GetFilms();
            var availableFilms = films.Where(f => f.IsAvailable).ToList();

            var filmDtos = await Task.WhenAll(availableFilms.Select(async film => new FilmDto
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

        public async Task<FilmDto> GetFilmById(int id)
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

       
        
        public async Task<bool> CreateEmprunt(int idFilmPartenaire)
        {
            var film = await _filmsRepository.GetFilmById(idFilmPartenaire);
            if (film == null || !film.IsAvailable) return false;

            film.IsAvailable = false;
            await _filmsRepository.UpdateFilm(film.Id, film);
            return true;
        }
        
        public async Task<bool> CancelEmprunt(string titreFilm)
        {
            var film = await _filmsRepository.GetFilmByTitle(titreFilm);
            if (film == null || film.IsAvailable) return false;

            film.IsAvailable = true;
            await _filmsRepository.UpdateFilm(film.Id, film);
            return true;
        }
        
        public async Task<List<FilmDispoDto>> GetAvailableFilmsByHost(int idHost)
        {
            var response = await _httpClient.GetAsync($"http://{idHost}/emprunt/dispo");
            response.EnsureSuccessStatusCode();
            var films = await response.Content.ReadFromJsonAsync<List<FilmDispoDto>>();
            return films;
        }
        
        public async Task CreateEmpruntForHost(int idHost, int idFilmPartenaire)
        {
            var postResponse = await _httpClient.PostAsync($"http://{idHost}/emprunt/{idFilmPartenaire}", null);
            if (postResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var getResponse = await _httpClient.GetAsync($"http://{idHost}/emprunt/{idFilmPartenaire}");
                getResponse.EnsureSuccessStatusCode();

                var filmPartenaireViewModel = await getResponse.Content.ReadFromJsonAsync<FilmPartenaireViewModel>();
                var filmDto = filmPartenaireViewModel.Adapt<FilmDto>();

                if (filmDto != null)
                {
                    await _filmsRepository.AddFilm(filmDto);
                }
            }
        }
        
        
        
        
    }
}