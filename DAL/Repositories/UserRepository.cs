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
            SqlCommand command = new SqlCommand("SELECT * FROM user", conn);
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

        public User Find(int id)
        {
            User user = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [user] WHERE id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
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
            SqlCommand command = new SqlCommand("SELECT * FROM [user] WHERE email=@Email", conn);
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
            SqlCommand command = new SqlCommand("INSERT INTO [user] (email, password, salt, role) VALUES (@Email, @Password, @Salt, @Role)", conn);
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
            SqlCommand command = new SqlCommand("UPDATE user SET email = @Email, password = @Password, role = @Role WHERE id = @id");
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.PasswordHash);
            command.Parameters.AddWithValue("@Salt", user.Salt);
            command.Parameters.AddWithValue("@Role", (int)user.Role);
            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
