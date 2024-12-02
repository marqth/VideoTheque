using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRatings;

namespace VideoTheque.Businesses.AgeRatings
{
    public class AgeRatingsBusiness : IAgeRatingsBusiness
    {
        private readonly IAgeRatingsRepository _ageRatingDao;

        public AgeRatingsBusiness(IAgeRatingsRepository ageRatingDao)
        {
            _ageRatingDao = ageRatingDao;
        }

        public Task<List<AgeRatingDto>> GetAgeRatings() => _ageRatingDao.GetAgeRatings();

        public AgeRatingDto GetAgeRating(int id)
        {
            var ageRating = _ageRatingDao.GetAgeRating(id).Result;

            if (ageRating == null)
            {
                throw new NotFoundException($"AgeRating '{id}' not found");
            }

            return ageRating;
        }

        public AgeRatingDto InsertAgeRating(AgeRatingDto ageRating)
        {
            if (_ageRatingDao.InsertAgeRating(ageRating).IsFaulted)
            {
                throw new InternalErrorException($"Error inserting AgeRating {ageRating.Name}");
            }

            return ageRating;
        }

        public void UpdateAgeRating(int id, AgeRatingDto ageRating)
        {
            if (_ageRatingDao.UpdateAgeRating(id, ageRating).IsFaulted)
            {
                throw new InternalErrorException($"Error updating AgeRating {ageRating.Name}");
            }
        }

        public void DeleteAgeRating(int id)
        {
            if (_ageRatingDao.DeleteAgeRating(id).IsFaulted)
            {
                throw new InternalErrorException($"Error deleting AgeRating with id {id}");
            }
        }
    }
}