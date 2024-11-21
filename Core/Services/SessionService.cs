using Core.Models;

namespace Core.Services
{
    public interface ISessionService
    {
        bool IsSessionActive(string sessionId);

        Task<string?> AuthenticateUser(string login, string password);

        bool DeleteSession(string sessionId);
    }

    public class SessionService : ISessionService
    {
        private const string FILE_PATH = "Data.txt";

        private Dictionary<string, User> sessions = new Dictionary<string, User>();
        private Dictionary<string, User> validUsers = new Dictionary<string, User>();

        private readonly IEncryptionService encriptionService;

        public SessionService(IEncryptionService encriptionService, string filePath = FILE_PATH)
        {
            this.encriptionService = encriptionService;

            try
            {
                LoadUsers(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failure to open file:  {ex}");
            }
        }

        private void LoadUsers(string filePath)
        {
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    var login = parts[0].Trim();
                    var password = parts[1].Trim();
                    validUsers[login] = new User(login, password);
                }
            }
        }

        public bool IsSessionActive(string sessionId)
        {
            return sessions.ContainsKey(sessionId);
        }

        public async Task<string?> AuthenticateUser(string login, string password)
        {
            var encryptedPass = await encriptionService.EncryptAsync(login, password);

            if (validUsers.ContainsKey(login) && validUsers[login].Password == encryptedPass)
            {
                string sessionId = Guid.NewGuid().ToString();
                sessions[sessionId] = validUsers[login];
                return sessionId;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteSession(string sessionId)
        {
            return sessions.Remove(sessionId);
        }
    }
}
