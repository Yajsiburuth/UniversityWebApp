using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UniversityWebApp.Helper;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();
        public StudentRepository()
        {

        }

        public IEnumerable<Student> GetAll()
        {
            IEnumerable<Student> students = new List<Student>();
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
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
                        student.GuardianId = reader.GetInt32(5);
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
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
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
                        student.GuardianId = reader.GetInt32(5);
                        student.Nid = reader.GetString(6);
                        student.UserId = reader.GetInt32(7);
                        student.Status = (Status)reader.GetInt32(8);
                    }
                }
                _conn.Close();
            }
            return student;
        }

        public int Create(Student student)
        {
            int studentId = 0;
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO student (first_name, last_name, phone_number, date_of_birth, guardian_id, national_id, user_id, status) " +
                    "VALUES (@FirstName, @LastName, @PhoneNumber, @DoB, @GuardianId, @Nid, @UserId, @Status); " +
                    "SELECT SCOPE_IDENTITY();"
                    , _conn);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@DoB", student.DoB);
                command.Parameters.AddWithValue("@GuardianId", student.GuardianId);
                command.Parameters.AddWithValue("@Nid", student.Nid);
                command.Parameters.AddWithValue("@UserId", student.UserId);
                command.Parameters.AddWithValue("@Status", (int)Status.Waiting);

                object returnObj = command.ExecuteScalar();
                if(returnObj != null)
                {
                    studentId = int.Parse(returnObj.ToString());
                }
                _conn.Close();
            }
            return studentId;
        }


        public void Update(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("UPDATE student SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, date_of_birth=@DoB, guardian_name=@GuardianName, national_id=@Nid");
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@DoB", student.DoB);
                command.Parameters.AddWithValue("@GuardianId", student.GuardianId);
                command.Parameters.AddWithValue("@Nid", student.Nid);
                int rows = command.ExecuteNonQuery();
                _conn.Close();
            }
        }
    }
}
