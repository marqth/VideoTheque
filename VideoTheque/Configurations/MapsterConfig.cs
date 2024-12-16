using Mapster;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Configurations
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<FilmDto, FilmViewModel>.NewConfig()
                .Map(dest => dest.MainActor, src => GetFullName(src.FirstActor.FirstName, src.FirstActor.LastName))
                .Map(dest => dest.Director, src => GetFullName(src.Director.FirstName, src.Director.LastName))
                .Map(dest => dest.Scenarist, src => GetFullName(src.Scenarist.FirstName, src.Scenarist.LastName))
                .Map(dest => dest.AgeRating, src => src.AgeRating.Name)
                .Map(dest => dest.Genre, src => src.Genre.Name)
                .Map(dest => dest.Support, src => "BluRay");

            TypeAdapterConfig<FilmViewModel, FilmDto>.NewConfig()
                .Map(dest => dest.FirstActor, src => new PersonneDto { FirstName = GetFirstName(src.MainActor), LastName = GetLastName(src.MainActor), Nationality = "Unknown", BirthDay = DateTime.MinValue })
                .Map(dest => dest.Director, src => new PersonneDto { FirstName = GetFirstName(src.Director), LastName = GetLastName(src.Director), Nationality = "Unknown", BirthDay = DateTime.MinValue })
                .Map(dest => dest.Scenarist, src => new PersonneDto { FirstName = GetFirstName(src.Scenarist), LastName = GetLastName(src.Scenarist), Nationality = "Unknown", BirthDay = DateTime.MinValue })
                .Map(dest => dest.AgeRating, src => new AgeRatingDto { Name = src.AgeRating })
                .Map(dest => dest.Genre, src => new GenreDto { Name = src.Genre });
        }

        private static string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}".Trim();
        }

        private static string GetFirstName(string fullName)
        {
            return fullName?.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
        }

        private static string GetLastName(string fullName)
        {
            var parts = fullName?.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            return parts?.Length > 1 ? parts[1] : string.Empty;
        }
    }
}