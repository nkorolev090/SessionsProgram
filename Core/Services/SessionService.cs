using Core.Interfaces;
using Core.Models;
using Domain.Interfaces;

namespace Core.Services
{
    public class SessionService : ISessionService
    {
        private Dictionary<string, User> sessions = new Dictionary<string, User>();

        private readonly IEncryptionService encriptionService;
        private readonly IUserRepository userRepository;

        public SessionService(IEncryptionService encriptionService, IUserRepository userRepository)
        {
            this.encriptionService = encriptionService;
            this.userRepository = userRepository;
        }

        public bool IsSessionActive(string sessionId)
        {
            return sessions.ContainsKey(sessionId);
        }

        public async Task<string?> AuthenticateUser(string login, string password)
        {
            var encryptedPass = await encriptionService.EncryptAsync(login, password);
            if (encryptedPass == null) return null;

            var user = await userRepository.Get(login, encryptedPass!);
            if (user == null) return null;

            string sessionId = Guid.NewGuid().ToString();
            sessions[sessionId] = new User(user);
            return sessionId;
        }

        public bool DeleteSession(string sessionId)
        {
            return sessions.Remove(sessionId);
        }
    }
}
