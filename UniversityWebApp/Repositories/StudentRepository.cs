using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using UniversityWebApp.Helper;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DatabaseHelper databaseHelper;

        public StudentRepository(DatabaseHelper dH)
        {
            databaseHelper = dH;
        }
        public IEnumerable<Student> GetAll()
        {
            IEnumerable<Student> students = new List<Student>();
            using (SqlConnection _conn = databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM student");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Student student = null;
                    while (reader.Read())
                    {
                        student.StudentId = reader.GetInt32(0);
                        student.FirstName = reader.GetString(1);
                        student.LastName = reader.GetString(2);
                        student.PhoneNumber = reader.GetString(3);
                        student.DoB = reader.GetDateTime(4);
                        student.GuardianName = reader.GetString(5);
                        student.Nid = reader.GetString(6);
                        student.UserId = reader.GetInt32(7);
                        student.Status = (Status)reader.GetInt32(8);
                        students.Append(student);
                    }
                }
                _conn.Close();
            }
            return students;
        }
        public Student Find(int id)
        {
            Student student = null;
            using (SqlConnection _conn = databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM student WHERE id = @id");
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        student.StudentId = reader.GetInt32(0);
                        student.FirstName = reader.GetString(1);
                        student.LastName = reader.GetString(2);
                        student.PhoneNumber = reader.GetString(3);
                        student.DoB = reader.GetDateTime(4);
                        student.GuardianName = reader.GetString(5);
                        student.Nid = reader.GetString(6);
                        student.UserId = reader.GetInt32(7);
                        student.Status = (Status)reader.GetInt32(8);
                    }
                }
                _conn.Close();
            }
            return student;
        }

        public void Create(Student student)
        {
            using (SqlConnection _conn = databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO student (first_name, last_name, phone_number, date_of_birth, guardian_name, national_id, user_id, status) " +
                    "INSERTED.Id" +
                    "VALUES (@FirstName, @LastName, @PhoneNumber, @DoB, @GuardianName, @Nid");
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@DoB", student.DoB);
                command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                command.Parameters.AddWithValue("@Nid", student.Nid);
                command.Parameters.AddWithValue("@user_id", student.UserId);
                command.Parameters.AddWithValue("@status", (int)Status.Waiting);

                int rows = command.ExecuteNonQuery();                
                _conn.Close();
            }
        }


        public void Update(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            using (SqlConnection _conn = databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("UPDATE student SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, date_of_birth=@DoB, guardian_name=@GuardianName, national_id=@Nid");
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@DoB", student.DoB);
                command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                command.Parameters.AddWithValue("@Nid", student.Nid);
                int rows = command.ExecuteNonQuery();
                _conn.Close();
            }
        }
    }
}
