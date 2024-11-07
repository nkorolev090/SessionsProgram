namespace SessionManagementApp
{
    class User
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    class SessionManager
    {
        private Dictionary<string, User> sessions = new Dictionary<string, User>();
        private Dictionary<string, User> validUsers = new Dictionary<string, User>();

        public SessionManager(string filePath)
        {
            LoadUsers(filePath);
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

        public string AuthenticateUser(string login, string password)
        {
            if (validUsers.ContainsKey(login) && validUsers[login].Password == password)
            {
                string sessionId = Guid.NewGuid().ToString();
                sessions[sessionId] = validUsers[login];
                Console.WriteLine($"Сессия создана с идентификатором: {sessionId}");
                return sessionId;
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль.");
                return null;
            }
        }

        public void DeleteSession(string sessionId)
        {
            if (sessions.Remove(sessionId))
            {
                Console.WriteLine($"Сессия с идентификатором {sessionId} удалена.");
            }
            else
            {
                Console.WriteLine("Сессия не найдена.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу с логинами и паролями:");
            string filePath = Console.ReadLine();
            try
            {
                var sessionManager = new SessionManager(filePath);

                while (true)
                {
                    Console.WriteLine("Введите идентификатор сессии (или 'delete <sessionId>' для удаления):");
                    string input = Console.ReadLine();

                    if (input.StartsWith("delete "))
                    {
                        string sessionIdToDelete = input.Substring(7).Trim();
                        sessionManager.DeleteSession(sessionIdToDelete);
                    }
                    else
                    {
                        string sessionId = input.Trim();

                        if (sessionManager.IsSessionActive(sessionId))
                        {
                            Console.WriteLine("Вы уже вошли в систему.");
                        }
                        else
                        {
                            Console.WriteLine("Введите логин:");
                            string login = Console.ReadLine();
                            Console.WriteLine("Введите пароль:");
                            string password = Console.ReadLine();

                            string newSessionId = sessionManager.AuthenticateUser(login, password);
                            if (!string.IsNullOrEmpty(newSessionId))
                            {
                                Console.WriteLine($"Ваша новая сессия: {newSessionId}");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Произошла ошибка {ex.Message}");
            }

        }
    }
}
