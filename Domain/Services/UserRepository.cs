using Core.Models;
using Dapper;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Domain.Services
{
    public class UserRepository: IUserRepository
    {
        private string connectionString;
        public UserRepository(string conn)
        {
            connectionString = conn;
        }

        public async Task<UserDBO?> Get(string login, string password)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var queryResult = await db.QueryAsync<UserDBO>("SELECT * FROM [User] WHERE Login = @login AND Password = @password", new { login, password });
                return queryResult.FirstOrDefault();
            }
        }
    }
}
