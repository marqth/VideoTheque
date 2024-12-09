using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.AgeRatings
{
    public class AgeRatingsRepository : IAgeRatingsRepository
    {
        private readonly VideothequeDb _db;

        public AgeRatingsRepository(VideothequeDb db)
        {
            _db = db;
        }

        public Task<List<AgeRatingDto>> GetAgeRatings() => _db.AgeRatings.ToListAsync();

        public ValueTask<AgeRatingDto?> GetAgeRating(int id) => _db.AgeRatings.FindAsync(id);

        public Task InsertAgeRating(AgeRatingDto ageRating)
        {
            _db.AgeRatings.AddAsync(ageRating);
            return _db.SaveChangesAsync();
        }
        
        public async Task<AgeRatingDto?> GetAgeRatingByName(string name)
        {
            return await _db.AgeRatings.FirstOrDefaultAsync(ar => ar.Name == name);
        }

        public Task UpdateAgeRating(int id, AgeRatingDto ageRating)
        {
            var ageRatingToUpdate = _db.AgeRatings.FindAsync(id).Result;

            if (ageRatingToUpdate is null)
            {
                throw new KeyNotFoundException($"AgeRating '{id}' not found");
            }

            ageRatingToUpdate.Name = ageRating.Name;
            ageRatingToUpdate.Abreviation = ageRating.Abreviation;
            return _db.SaveChangesAsync();
        }

        public Task DeleteAgeRating(int id)
        {
            var ageRatingToDelete = _db.AgeRatings.FindAsync(id).Result;

            if (ageRatingToDelete is null)
            {
                throw new KeyNotFoundException($"AgeRating '{id}' not found");
            }

            _db.AgeRatings.Remove(ageRatingToDelete);
            return _db.SaveChangesAsync();
        }
    }
}