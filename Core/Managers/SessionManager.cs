using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Managers
{
    public interface ISessionManager
    {
        bool IsSessionActive(string sessionId);

        string? AuthenticateUser(string login, string password);

        bool DeleteSession(string sessionId);
    }

    public class SessionManager : ISessionManager
    {
        private const string FILE_PATH = "Data.txt";

        private Dictionary<string, User> sessions = new Dictionary<string, User>();
        private Dictionary<string, User> validUsers = new Dictionary<string, User>();

        public SessionManager(string filePath = FILE_PATH)
        {
            try
            {
                LoadUsers(filePath);
            }
            catch (Exception ex)
            {

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

        public string? AuthenticateUser(string login, string password)
        {
            if (validUsers.ContainsKey(login) && validUsers[login].Password == password)
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
