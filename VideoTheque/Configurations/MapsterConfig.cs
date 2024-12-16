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
                .Map(dest => dest.FirstActor.FirstName, src => GetFirstName(src.MainActor))
                .Map(dest => dest.FirstActor.LastName, src => GetLastName(src.MainActor))
                .Map(dest => dest.Director.FirstName, src => GetFirstName(src.Director))
                .Map(dest => dest.Director.LastName, src => GetLastName(src.Director))
                .Map(dest => dest.Scenarist.FirstName, src => GetFirstName(src.Scenarist))
                .Map(dest => dest.Scenarist.LastName, src => GetLastName(src.Scenarist))
                .Map(dest => dest.AgeRating.Name, src => src.AgeRating)
                .Map(dest => dest.Genre.Name, src => src.Genre);
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