using Core.Models;
namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDBO?> Get(string login, string password);
    }
}
