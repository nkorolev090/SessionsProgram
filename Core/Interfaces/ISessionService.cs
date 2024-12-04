namespace Core.Interfaces
{
    public interface ISessionService
    {
        bool IsSessionActive(string sessionId);

        Task<string?> AuthenticateUser(string login, string password);

        bool DeleteSession(string sessionId);
    }
}
