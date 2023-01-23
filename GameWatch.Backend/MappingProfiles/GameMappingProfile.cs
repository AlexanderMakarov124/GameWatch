using AutoMapper;
using GameWatch.Domain.Entities;
using GameWatch.UseCases.DTOs;

namespace GameWatch.Backend.MappingProfiles;

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
        CreateMap<GameDto, Game>().ForMember(destination => destination.CreatedAt, options => options.MapFrom(date => DateTime.Now));
    }
}
