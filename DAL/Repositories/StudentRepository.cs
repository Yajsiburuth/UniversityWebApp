using DAL.Models;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class StudentRepository : DatabaseHelper, IRepository<Student>
    {
        public IEnumerable<Student> GetAll()
        {
            IEnumerable<Student> students = new List<Student>();
            SqlCommand command = new SqlCommand("SELECT * FROM student", conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Append(
                    new Student()
                    {
                        StudentId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        DoB = reader.GetDateTime(4),
                        GuardianName = reader.GetString(5),
                        Nid = reader.GetString(6),
                        UserId = reader.GetInt32(7),
                        Status = (Status)reader.GetInt32(8),
                    });
            }
            reader.Close();
            command.Dispose();
            return students;
        }

        public Student Find(int id)
        {
            Student student = null;
            SqlCommand command = new SqlCommand("SELECT * FROM student WHERE id = @Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = command.ExecuteReader();
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
            reader.Close();
            command.Dispose();
            return student;
        }

        public int Create(Student student)
        {
            int studentId = 0;
            SqlCommand command = new SqlCommand("INSERT INTO student (first_name, last_name, phone_number, date_of_birth, guardian_name, national_id, user_id, status) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @DoB, @GuardianName, @Nid, @UserId, @Status); " +
                "SELECT SCOPE_IDENTITY();"
                , conn);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@DoB", student.DoB);
            command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
            command.Parameters.AddWithValue("@Nid", student.Nid);
            command.Parameters.AddWithValue("@UserId", student.UserId);
            command.Parameters.AddWithValue("@Status", (int)Status.Waiting);

            object returnObj = command.ExecuteScalar();
            if(returnObj != null)
                studentId = int.Parse(returnObj.ToString());

            command.Dispose();
            return studentId;
        }


        public void Update(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            SqlCommand command = new SqlCommand("UPDATE student SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, date_of_birth=@DoB, guardian_name=@GuardianName, national_id=@Nid, user_id=@UserId, status=@Status");
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@DoB", student.DoB);
            command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
            command.Parameters.AddWithValue("@Nid", student.Nid);
            command.Parameters.AddWithValue("@UserId", student.UserId);
            command.Parameters.AddWithValue("@Status", student.Status);

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
