// File: VideoTheque/Configurations/MapsterConfig.cs
using Mapster;
using VideoTheque.Businesses.Films;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Configurations
{
    public static class MapsterConfig
    {
        public static void RegisterMappings(IFilmsBusiness filmsBusiness)
        {
            TypeAdapterConfig<FilmDto, FilmViewModel>.NewConfig()
                .Map(dest => dest.MainActor, src => filmsBusiness.GetPersonNameById(src.IdFirstActor).Result)
                .Map(dest => dest.Director, src => filmsBusiness.GetPersonNameById(src.IdDirector).Result)
                .Map(dest => dest.Scenarist, src => filmsBusiness.GetPersonNameById(src.IdScenarist).Result)
                .Map(dest => dest.AgeRating, src => filmsBusiness.GetAgeRatingNameById(src.IdAgeRating).Result)
                .Map(dest => dest.Genre, src => filmsBusiness.GetGenreNameById(src.IdGenre).Result)
                .Map(dest => dest.Support, src => "BluRay");

            TypeAdapterConfig<FilmViewModel, FilmDto>.NewConfig()
                .Map(dest => dest.IdFirstActor, src => filmsBusiness.GetPersonIdByName(src.MainActor).Result)
                .Map(dest => dest.IdDirector, src => filmsBusiness.GetPersonIdByName(src.Director).Result)
                .Map(dest => dest.IdScenarist, src => filmsBusiness.GetPersonIdByName(src.Scenarist).Result)
                .Map(dest => dest.IdAgeRating, src => filmsBusiness.GetAgeRatingIdByName(src.AgeRating).Result)
                .Map(dest => dest.IdGenre, src => filmsBusiness.GetGenreIdByName(src.Genre).Result);
        }
    }
}