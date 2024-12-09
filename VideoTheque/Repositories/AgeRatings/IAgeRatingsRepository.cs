namespace VideoTheque.Repositories.AgeRatings
{
    using VideoTheque.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAgeRatingsRepository
    {
        Task<List<AgeRatingDto>> GetAgeRatings();
        ValueTask<AgeRatingDto?> GetAgeRating(int id);
        Task InsertAgeRating(AgeRatingDto ageRating);
        Task UpdateAgeRating(int id, AgeRatingDto ageRating);
        Task DeleteAgeRating(int id);
        Task<AgeRatingDto?> GetAgeRatingByName(string name);
    }
}