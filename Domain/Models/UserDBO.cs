namespace Core.Models
{
    public class UserDBO
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
