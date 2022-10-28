﻿using DAL.Models;
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
            SqlCommand command = new SqlCommand("SELECT StudentId, FirstName, LastName, PhoneNumber, DateOfBirth, GuardianName, NationalId, UserId, Status FROM Student", conn);
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
                        DateOfBirth = reader.GetDateTime(4),
                        GuardianName = reader.GetString(5),
                        NationalId = reader.GetString(6),
                        UserId = reader.GetInt32(7),
                        Status = (Status)reader.GetInt32(8),
                    });
            }
            reader.Close();
            command.Dispose();
            return students;
        }

        public Student Find(int studentId)
        {
            Student student = null;
            SqlCommand command = new SqlCommand("SELECT StudentId, FirstName, LastName, PhoneNumber, DateOfBirth, GuardianName, NationalId, UserId, Status FROM Student WHERE StudentId = @StudentId", conn);
            command.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                student.StudentId = reader.GetInt32(0);
                student.FirstName = reader.GetString(1);
                student.LastName = reader.GetString(2);
                student.PhoneNumber = reader.GetString(3);
                student.DateOfBirth = reader.GetDateTime(4);
                student.GuardianName = reader.GetString(5);
                student.NationalId = reader.GetString(6);
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
            SqlCommand command = new SqlCommand("INSERT INTO student (FirstName, LastName, PhoneNumber, DateOfBirth, GuardianName, NationalId, UserId, Status) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @DateOfBirth, @GuardianName, @NationalId, @UserId, @Status); " +
                "SELECT SCOPE_IDENTITY();"
                , conn);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
            command.Parameters.AddWithValue("@NationalId", student.NationalId);
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

            SqlCommand command = new SqlCommand("UPDATE student SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, date_of_birth=@DateOfBirth, guardian_name=@GuardianName, national_id=@NationalId, user_id=@UserId, status=@Status");
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
            command.Parameters.AddWithValue("@NationalId", student.NationalId);
            command.Parameters.AddWithValue("@UserId", student.UserId);
            command.Parameters.AddWithValue("@Status", student.Status);

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
