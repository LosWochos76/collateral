namespace RequestReply.Shared;

public record FindUserQuery(string Email);
public record FindUserResponse(User User);
public record FindUserError(string errorMessage);
