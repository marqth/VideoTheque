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
                .Map(dest => dest.MainActor, src => src.FirstActor.FirstName + " " + src.FirstActor.LastName)
                .Map(dest => dest.Director, src => src.Director.FirstName + " " + src.Director.LastName)
                .Map(dest => dest.Scenarist, src => src.Scenarist.FirstName + " " + src.Scenarist.LastName)
                .Map(dest => dest.AgeRating, src => src.AgeRating.Name)
                .Map(dest => dest.Genre, src => src.Genre.Name)
                .Map(dest => dest.Support, src => "BluRay");

            TypeAdapterConfig<FilmViewModel, FilmDto>.NewConfig()
                .Map(dest => dest.FirstActor.FirstName + " " + dest.FirstActor.LastName, src => src.MainActor)
                .Map(dest => dest.FirstActor.FirstName + " " + dest.FirstActor.LastName, src => src.Director)
                .Map(dest => dest.FirstActor.FirstName + " " + dest.FirstActor.LastName, src => src.Scenarist)
                .Map(dest => dest.AgeRating.Name, src => src.AgeRating)
                .Map(dest => dest.Genre.Name, src => src.Genre);
        }
    }
}