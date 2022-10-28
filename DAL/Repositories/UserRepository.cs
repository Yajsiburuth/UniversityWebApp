using DAL.Models;
using Helpers.Helper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : DatabaseHelper, IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = new List<User>();
            SqlCommand command = new SqlCommand("SELECT UserId, Email, PasswordHash, Salt, Role FROM User", conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Append(new User()
                {
                    UserId = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    PasswordHash = (byte[])reader.GetValue(2),
                    Salt = (byte[])reader.GetValue(3),
                    Role = (Role)reader.GetInt32(4)
                });
            }
            reader.Close();
            command.Dispose();
            return users;
        }

        public User Find(int userId)
        {
            User user = null;
            SqlCommand command = new SqlCommand("SELECT UserId, Email, PasswordHash, Salt, Role FROM User WHERE UserId=@UserId", conn);
            command.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user = new User()
                {
                    UserId = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    PasswordHash = (byte[])reader.GetValue(2),
                    Salt = (byte[])reader.GetValue(3),
                    Role = (Role)reader.GetByte(4),
                };
            }
            reader.Close();
            command.Dispose();
            return user;
        }

        public User Find(string email)
        {
            User user = null;
            SqlCommand command = new SqlCommand("SELECT UserId, Email, PasswordHash, Salt, Role FROM [User] WHERE Email=@Email", conn);
            command.Parameters.AddWithValue("@Email", email);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user = new User()
                {
                    UserId = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    PasswordHash = (byte[])reader.GetValue(2),
                    Salt = (byte[])reader.GetValue(3),
                    Role = (Role)reader.GetByte(4),
                };
            }
            reader.Close();
            command.Dispose();
            return user;
        }

        public int Create(User user)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [User] (Email, PasswordHash, Salt, Role) VALUES (@Email, @Password, @Salt, @Role)", conn);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.PasswordHash);
            command.Parameters.AddWithValue("@Salt", user.Salt);
            command.Parameters.AddWithValue("@Role", (int)user.Role);
            int rows = command.ExecuteNonQuery();
            command.Dispose();
            return rows;
        }

        public void Update(User user)
        {
            SqlCommand command = new SqlCommand("UPDATE User SET Email = @Email, PasswordHash = @Password, Role = @Role WHERE Id = @Id");
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.PasswordHash);
            command.Parameters.AddWithValue("@Salt", user.Salt);
            command.Parameters.AddWithValue("@Role", (int)user.Role);
            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
