using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Films
{
    public class FilmsRepository : IFilmsRepository
    {
        private readonly VideothequeDb _db;

        public FilmsRepository(VideothequeDb db)
        {
            _db = db;
        }

        public async Task<List<FilmDto>> GetFilms()
        {
            var bluRays = await _db.BluRays.ToListAsync();
            return bluRays.Select(b => new FilmDto
            {
                Id = b.Id,
                Title = b.Title,
                Duration = b.Duration,
                IdFirstActor = b.IdFirstActor,
                IdDirector = b.IdDirector,
                IdScenarist = b.IdScenarist,
                IdAgeRating = b.IdAgeRating,
                IdGenre = b.IdGenre,
                IsAvailable = b.IsAvailable,
                IdOwner = b.IdOwner
            }).ToList();
        }

        public async Task<FilmDto?> GetFilmById(int id)
        {
            var bluRay = await _db.BluRays.FindAsync(id);
            if (bluRay == null) return null;

            return new FilmDto
            {
                Id = bluRay.Id,
                Title = bluRay.Title,
                Duration = bluRay.Duration,
                IdFirstActor = bluRay.IdFirstActor,
                IdDirector = bluRay.IdDirector,
                IdScenarist = bluRay.IdScenarist,
                IdAgeRating = bluRay.IdAgeRating,
                IdGenre = bluRay.IdGenre,
                IsAvailable = bluRay.IsAvailable,
                IdOwner = bluRay.IdOwner
            };
        }

        public async Task InsertFilm(FilmDto film)
        {
            var bluRay = new BluRayDto
            {
                Id = film.Id,
                Title = film.Title,
                Duration = film.Duration,
                IdFirstActor = film.IdFirstActor,
                IdDirector = film.IdDirector,
                IdScenarist = film.IdScenarist,
                IdAgeRating = film.IdAgeRating,
                IdGenre = film.IdGenre,
                IsAvailable = film.IsAvailable,
                IdOwner = film.IdOwner
            };
            await _db.BluRays.AddAsync(bluRay);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateFilm(int id, FilmDto film)
        {
            var filmToUpdate = await _db.BluRays.FindAsync(id);

            if (filmToUpdate is null)
            {
                throw new KeyNotFoundException($"Film '{id}' not found");
            }

            filmToUpdate.Title = film.Title;
            filmToUpdate.Duration = film.Duration;
            filmToUpdate.IdFirstActor = film.IdFirstActor;
            filmToUpdate.IdDirector = film.IdDirector;
            filmToUpdate.IdScenarist = film.IdScenarist;
            filmToUpdate.IdAgeRating = film.IdAgeRating;
            filmToUpdate.IdGenre = film.IdGenre;
            filmToUpdate.IsAvailable = film.IsAvailable;
            filmToUpdate.IdOwner = film.IdOwner;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFilm(int id)
        {
            var filmToDelete = await _db.BluRays.FindAsync(id);

            if (filmToDelete is null)
            {
                throw new KeyNotFoundException($"Film '{id}' not found");
            }

            _db.BluRays.Remove(filmToDelete);
            await _db.SaveChangesAsync();
        }
    }
}