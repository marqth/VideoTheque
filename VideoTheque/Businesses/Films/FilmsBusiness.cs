using VideoTheque.DTOs;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.AgeRatings;
using VideoTheque.Repositories.Hosts;
using VideoTheque.ViewModels;
using System.Net.Http.Json;
using Mapster;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IFilmsRepository _filmsRepository;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IGenresRepository _genreRepository;
        private readonly IAgeRatingsRepository _ageRatingRepository;
        private readonly IHostsRepository _hostRepository;
        private readonly HttpClient _httpClient;

        public FilmsBusiness(IFilmsRepository filmsRepository, IPersonnesRepository personnesRepository, IGenresRepository genreRepository, IAgeRatingsRepository ageRatingRepository,  HttpClient httpClient, IHostsRepository hostsRepository)
        {
            _filmsRepository = filmsRepository;
            _personnesRepository = personnesRepository;
            _genreRepository = genreRepository;
            _ageRatingRepository = ageRatingRepository;
            _httpClient = httpClient;
            _hostRepository = hostsRepository;
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
                IdFirstActor = await GetPersonIdByName(filmDto.FirstActor.FirstName + " " + filmDto.FirstActor.LastName),
                IdDirector = await GetPersonIdByName(filmDto.Director.FirstName + " " + filmDto.Director.LastName),
                IdScenarist = await GetPersonIdByName(filmDto.Scenarist.FirstName + " " + filmDto.Scenarist.LastName),
                IdAgeRating = await GetAgeRatingIdByName(filmDto.AgeRating.Name),
                IdGenre = await GetGenreIdByName(filmDto.Genre.Name),
                IsAvailable = true,
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
                IdFirstActor = await GetPersonIdByName(filmDto.FirstActor.FirstName + " " + filmDto.FirstActor.LastName),
                IdDirector = await GetPersonIdByName(filmDto.Director.FirstName + " " + filmDto.Director.LastName),
                IdScenarist = await GetPersonIdByName(filmDto.Scenarist.FirstName + " " + filmDto.Scenarist.LastName),
                IdAgeRating = await GetAgeRatingIdByName(filmDto.AgeRating.Name),
                IdGenre = await GetGenreIdByName(filmDto.Genre.Name),
                IsAvailable = filmDto.IsAvailable,
                IdOwner = filmDto.IdOwner
            };

            await _filmsRepository.UpdateFilm(id, bluRayDto);
        }
        
        public async Task<List<FilmDispoDto>> GetAvailableFilmsByHost(int idHost)
        {
            var host = await _hostRepository.GetHost(idHost);
            if (host == null)
            {
                throw new Exception("Host not found");
            }

            var response = await _httpClient.GetAsync($"http://{host.Url}:5000/emprunt/dispo");
            response.EnsureSuccessStatusCode();
            var films = await response.Content.ReadFromJsonAsync<List<FilmDispoDto>>();
            return films;
        }
        
        public async Task CreateEmpruntForHost(int idHost, int idFilmPartenaire)
        {
            var host = await _hostRepository.GetHost(idHost);
            if (host == null)
            {
                throw new Exception("Host not found");
            }
            
            var postResponse = await _httpClient.PostAsync($"http://{host.Url}:5000/emprunt/{idFilmPartenaire}", null);
            if (postResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var getResponse = await _httpClient.GetAsync($"http://{host.Url}:5000/emprunt/{idFilmPartenaire}");
                getResponse.EnsureSuccessStatusCode();

                var filmPartenaireViewModel = await getResponse.Content.ReadFromJsonAsync<FilmPartenaireViewModel>();
                var filmDto = filmPartenaireViewModel.Adapt<FilmDto>();

                if (filmDto != null)
                {
                    filmDto.IdOwner = idHost;
                    filmDto.IsAvailable = false;

                    // Check and add FirstActor
                    var firstActor = await _personnesRepository.GetPersonneByLastNameAndFirstName(filmDto.FirstActor.LastName, filmDto.FirstActor.FirstName);
                    if (firstActor == null)
                    {
                        await _personnesRepository.InsertPersonne(filmDto.FirstActor);
                    }
                    else
                    {
                        filmDto.FirstActor = firstActor;
                    }

                    // Check and add Director
                    var director = await _personnesRepository.GetPersonneByLastNameAndFirstName(filmDto.Director.LastName, filmDto.Director.FirstName);
                    if (director == null)
                    {
                        await _personnesRepository.InsertPersonne(filmDto.Director);
                    }
                    else
                    {
                        filmDto.Director = director;
                    }

                    // Check and add Scenarist
                    var scenarist = await _personnesRepository.GetPersonneByLastNameAndFirstName(filmDto.Scenarist.LastName, filmDto.Scenarist.FirstName);
                    if (scenarist == null)
                    {
                        await _personnesRepository.InsertPersonne(filmDto.Scenarist);
                    }
                    else
                    {
                        filmDto.Scenarist = scenarist;
                    }

                    // Check and add Genre
                    var genre = await _genreRepository.GetGenreByName(filmDto.Genre.Name);
                    if (genre == null)
                    {
                        await _genreRepository.InsertGenre(filmDto.Genre);
                    }
                    else
                    {
                        filmDto.Genre = genre;
                    }

                    // Check and add AgeRating
                    var ageRating = await _ageRatingRepository.GetAgeRatingByAbbreviation(filmDto.AgeRating.Abreviation);
                    if (ageRating == null)
                    {
                        await _ageRatingRepository.InsertAgeRating(filmDto.AgeRating);
                    }
                    else
                    {
                        filmDto.AgeRating = ageRating;
                    }
                    
                    var bluRayDto = new BluRayDto
                    {
                        Id = filmDto.Id,
                        Title = filmDto.Title,
                        Duration = filmDto.Duration,
                        IdFirstActor = await GetPersonIdByName(filmDto.FirstActor.FirstName + " " + filmDto.FirstActor.LastName),
                        IdDirector = await GetPersonIdByName(filmDto.Director.FirstName + " " + filmDto.Director.LastName),
                        IdScenarist = await GetPersonIdByName(filmDto.Scenarist.FirstName + " " + filmDto.Scenarist.LastName),
                        IdAgeRating = await GetAgeRatingIdByName(filmDto.AgeRating.Name),
                        IdGenre = await GetGenreIdByName(filmDto.Genre.Name),
                        IsAvailable = filmDto.IsAvailable,
                        IdOwner = filmDto.IdOwner
                    };

                    await _filmsRepository.InsertFilm(bluRayDto);
                }
            }
        }

        public async Task DeleteFilm(int IdFilm)
        {
            // Retrieve the film from the database
            var film = await _filmsRepository.GetFilmById(IdFilm);
            if (film == null)
            {
                throw new Exception("Film not found");
            }

            // Retrieve the host from the database using IdOwner
            var host = film.IdOwner.HasValue ? await _hostRepository.GetHost(film.IdOwner.Value) : null;
            var titreFilm = film.Title;
            
            if (film.IsAvailable == false & host==null)
            {
                throw new Exception("Film Emprunté}");
            }
    
            if (host != null)
            {
                var deleteResponse = await _httpClient.DeleteAsync($"http://{host.Url}:5000/emprunt/{titreFilm}");
                if (deleteResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new Exception($"Failed to delete the film from the host {film.IdOwner.Value} ");
                }
            }

      

            await _filmsRepository.DeleteFilm(IdFilm);
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