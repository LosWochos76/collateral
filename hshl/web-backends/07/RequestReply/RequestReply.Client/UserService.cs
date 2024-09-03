using MassTransit;
using Microsoft.Extensions.Logging;
using RequestReply.Shared;

public class UserService(ILogger<UserService> logger, IRequestClient<FindUserQuery> client)
{
    public async Task<User> FindUserByEmailAsync(string email)
    {
        var request = new FindUserQuery(email);
        var result = await client.GetResponse<FindUserResponse, FindUserError>(request);

        if (result.Is(out Response<FindUserError> errorResponse))
        {
           logger.LogError("Could not login!");
           return null;
        }
        
        if (result.Is(out Response<FindUserResponse> userResponse))
        {
            logger.LogDebug("User logged in!");
            return userResponse.Message.User;
        }

         return null;
    }
}