using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Helpers;
using UniversityWebApp.Helper;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public UserRepository() { _databaseHelper = new DatabaseHelper(); }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM user", _conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.UserId = reader.GetInt32(0);
                    user.Email = reader.GetString(1);
                    user.PasswordHash = (byte[])reader.GetValue(2);
                    user.Salt = (byte[])reader.GetValue(3);
                    user.Role = (Role) reader.GetInt32(4);
                    users.Append(user);
                }
                _conn.Close();
            }
            return users;
        }

        public User Find(int id)
        {
            User user = new User();
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [user] WHERE id=@Id", _conn);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.Read())
                {
                    user.UserId = reader.GetInt32(0);
                    user.Email = reader.GetString(1);
                    user.PasswordHash = (byte[])reader.GetValue(2);
                    user.Salt = (byte[])reader.GetValue(3);
                    user.Role = Role.User;
                }
                _conn.Close();
            }
            return user;

        }

        public User Find(string email)
        {
            User user = new User();
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [user] WHERE email=@Email", _conn);
                command.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    return null;
                }
                    while (reader.Read())
                    {
                        user.UserId = reader.GetInt32(0);
                        user.Email = reader.GetString(1);
                        user.PasswordHash = (byte[]) reader.GetValue(2);
                        user.Salt = (byte[]) reader.GetValue(3);
                        user.Role = Role.User;
                    }
                _conn.Close();
                }
            return user;
        }

        public int Create(User user)
        {
            int rows = 0;
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [user] (email, password, salt, role) VALUES (@Email, @Password, @Salt, @Role)",_conn);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.PasswordHash);
                command.Parameters.AddWithValue("@Salt", user.Salt);
                command.Parameters.AddWithValue("@Role", (int)user.Role);
                rows = command.ExecuteNonQuery();
                _conn.Close();
            }
            return rows;

        }

        public void Update(User user)
        {
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("UPDATE user SET email = @Email, password = @Password, role = @Role WHERE id = @id");
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.PasswordHash);
                command.Parameters.AddWithValue("@Salt", user.Salt);
                command.Parameters.AddWithValue("@Role", (int)user.Role);
                int rows = command.ExecuteNonQuery();
                _conn.Close();
            }
        }
    }
}
