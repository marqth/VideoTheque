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

        public async Task<List<BluRayDto>> GetFilms()
        {
            return await _db.BluRays
                .Select(b => new BluRayDto
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
                })
                .ToListAsync();
        }

        public async Task<BluRayDto?> GetFilmById(int id)
        {
            var film = await _db.BluRays
                .FirstOrDefaultAsync(b => b.Id == id);

            if (film == null) return null;

            return new BluRayDto
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
        }

        public async Task InsertFilm(BluRayDto bluRay)
        {
            _db.BluRays.Add(bluRay);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateFilm(int id, BluRayDto bluRayDto)
        {
            var filmToUpdate = await _db.BluRays.FindAsync(id);

            if (filmToUpdate is null)
            {
                throw new KeyNotFoundException($"Film '{id}' not found");
            }

            filmToUpdate.Title = bluRayDto.Title;
            filmToUpdate.Duration = bluRayDto.Duration;
            filmToUpdate.IdFirstActor = bluRayDto.IdFirstActor;
            filmToUpdate.IdDirector = bluRayDto.IdDirector;
            filmToUpdate.IdScenarist = bluRayDto.IdScenarist;
            filmToUpdate.IdAgeRating = bluRayDto.IdAgeRating;
            filmToUpdate.IdGenre = bluRayDto.IdGenre;
            filmToUpdate.IsAvailable = bluRayDto.IsAvailable;
            filmToUpdate.IdOwner = bluRayDto.IdOwner;

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