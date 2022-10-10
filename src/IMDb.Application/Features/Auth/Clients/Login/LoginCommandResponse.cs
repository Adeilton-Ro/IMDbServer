namespace IMDb.Application.Features.Auth.Clients.Login;
public record LoginCommandResponse(string Name, string Email, string Token);