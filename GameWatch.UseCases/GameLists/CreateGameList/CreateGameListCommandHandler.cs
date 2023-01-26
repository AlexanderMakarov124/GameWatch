using AutoMapper;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.GameLists.CreateGameList;

/// <summary>
/// Handler to create game list command.
/// </summary>
public class CreateGameListCommandHandler : IRequestHandler<CreateGameListCommand, Unit>
{
    private readonly ApplicationContext db;
    private readonly ILogger<CreateGameListCommandHandler> logger;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateGameListCommandHandler(ApplicationContext db, ILogger<CreateGameListCommandHandler> logger, IMapper mapper)
    {
        this.db = db;
        this.logger = logger;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(CreateGameListCommand request, CancellationToken cancellationToken)
    {
        var gameListDto = request.GameListDto;

        if (await db.GameLists.AnyAsync(gl => gl.Name.ToLower().Equals(gameListDto.Name.ToLower()), cancellationToken))
        {
            logger
                .LogError("Can not create game list {Name} because game list with such name already exists.",
                gameListDto.Name);

            throw new AlreadyExistException(
                    $"Can not create game list {gameListDto.Name} because game list with such name already exists.");
        }

        var gameList = mapper.Map<GameList>(gameListDto);

        db.GameLists.Add(gameList);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug("Game list {Name} with id {Id} was successfully created.", gameList.Name, gameList.Id);

        return default;
    }
}
