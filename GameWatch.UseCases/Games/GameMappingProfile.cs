using AutoMapper;
using GameWatch.Domain.Entities;
using GameWatch.UseCases.Games.Queries.SearchGames;

namespace GameWatch.UseCases.Games;

/// <summary>
/// Game mapping profile.
/// </summary>
public class GameMappingProfile : Profile
{
    /// <summary>
    /// Constructor that contains mapping rules.
    /// </summary>
    public GameMappingProfile()
    {
        CreateMap<GameDto, Game>()
            .ReverseMap()
            .ForMember(dto => dto.Genres, options => options.MapFrom(entity => entity.Genres.Select(genre => genre.Name)));
    }
}
