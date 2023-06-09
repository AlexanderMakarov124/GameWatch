using GameWatch.DataAccess;
using IGDB;

namespace GameWatch.Infrastructure;

//internal class CustomTokenStore /*: ITokenStore*/
//{
//    private readonly ApplicationContext dbContext;

//    public CustomTokenStore(ApplicationContext dbContext)
//    {
//        this.dbContext = dbContext;
//    }

//    public Task<TwitchAccessToken> GetTokenAsync()
//    {
//        Get token from database, etc.
//        var token = // ...
//        return token;
//    }

//    public Task<TwitchAccessToken> StoreTokenAsync(TwitchAccessToken token)
//    {
//        Store new token in database, etc.
//        return token;
//    }
//}
