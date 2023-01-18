using AutoMapper;
using GameWatch.Domain.Entities;
using GameWatch.UseCases.DTOs;

namespace GameWatch.Backend.MappingProfiles;

/// <summary>
/// Game list mapping profile.
/// </summary>
public class GameListMappingProfile : Profile
{
    /// <summary>
    /// Constructor that contains mapping rules.
    /// </summary>
    public GameListMappingProfile()
    {
        CreateMap<GameListDto, GameList>();
    }
}
