using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameWatch.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.UseCases.Games.Queries.SearchGames;

/// <summary>
/// Handler for <see cref="SearchGamesQuery"/>.
/// </summary>
public class SearchGamesQueryHandler : IRequestHandler<SearchGamesQuery, IEnumerable<GameDto>>
{
    private readonly ApplicationContext db;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SearchGamesQueryHandler(ApplicationContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GameDto>> Handle(SearchGamesQuery request, CancellationToken cancellationToken)
    {
        var games = db.Games.AsQueryable();

        if (request.Name != null)
        {
            games = games.Where(g => g.Name.Contains(request.Name));
        }

        return await games
            .ProjectTo<GameDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
