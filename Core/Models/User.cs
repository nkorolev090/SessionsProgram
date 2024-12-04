namespace Core.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User(UserDBO user)
        {
            Login = user.Login;
            Password = user.Password;
        }
    }
}
