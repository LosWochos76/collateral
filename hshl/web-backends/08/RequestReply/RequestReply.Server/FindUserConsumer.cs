using MassTransit;
using RequestReply.Shared;

public class FindUserConsumer : IConsumer<FindUserQuery>
{
    public async Task Consume(ConsumeContext<FindUserQuery> context)
    {
        var query = context.Message; 
        if (query.Email.ToLower().Equals("hans.wurst@gmail.com"))
        {
            var reply = new FindUserResponse(new User() { Id = Guid.NewGuid(), Firstname="Hans", Lastname="Wurst", Email="hans.wurst@gmail.com" });
            await context.RespondAsync(reply);
            return;
        }

        await context.RespondAsync(new FindUserError("User not found!"));
    }
}