using DAL.Models;
using DAL.ViewModels;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class StudentRepository : DatabaseHelper, IStudentRepository
    {
        string selectSQL = $"SELECT {SqlHelper.GetColumnNames(typeof(Student))} FROM [Student] ";
        string insertSQL = $"INSERT INTO [Student] ({SqlHelper.GetColumnNames(typeof(Student), excludedProp: new HashSet<string>() { "StudentId" })}) VALUES ({SqlHelper.GetColumnNames(typeof(Student), parameter: true, excludedProp: new HashSet<string>() { "StudentId" })})";

        public IEnumerable<Student> GetAll()
        {
            IEnumerable<Student> students = new List<Student>();
            SqlCommand command = new SqlCommand(selectSQL, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Append(
                    new Student()
                    {
                        StudentId = (int)reader["StudentId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Address = (string)reader["Address"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        GuardianName = (string)reader["GuardianName"] ?? "",
                        NationalId = (string)reader["NationalId"],
                        UserId = (int)reader["UserId"],
                        Status = (Status)reader["Status"],
                    });
            }
            reader.Close();
            command.Dispose();
            
            return students;
        }

        public Student Find(int userId)
        {
            Student student = null;
            SqlCommand command = new SqlCommand(selectSQL + "WHERE UserId = @UserId", conn);
            command.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                student = new Student()
                {
                    StudentId = (int)reader["StudentId"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Address = (string)reader["Address"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    DateOfBirth = (DateTime)reader["DateOfBirth"],
                    GuardianName = (string)reader["GuardianName"] ?? "",
                    NationalId = (string)reader["NationalId"],
                    UserId = (int)reader["UserId"],
                    Status = (Status)reader["Status"],
                };
            }
            reader.Close();
            command.Dispose();
            
            return student;
        }

        public int Create(Student student)
        {
            int studentId = 0;
            SqlCommand command = new SqlCommand(insertSQL + "SELECT SCOPE_IDENTITY();", conn);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Address", student.Address);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
            command.Parameters.AddWithValue("@NationalId", student.NationalId);
            command.Parameters.AddWithValue("@UserId", student.UserId);
            command.Parameters.AddWithValue("@Status", (Int16)Status.Waiting);

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

            SqlCommand command = new SqlCommand("UPDATE student SET FirstName=@FirstName, LastName=@LastName, Address=@Address, PhoneNumber=@PhoneNumber, DateOfBirth=@DateOfBirth, GuardianName=@GuardianName, NationalId=@NationalId, UserId=@UserId, Status=@Status");
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Address", student.Address);
            command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
            command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
            command.Parameters.AddWithValue("@NationalId", student.NationalId);
            command.Parameters.AddWithValue("@UserId", student.UserId);
            command.Parameters.AddWithValue("@Status", student.Status);

            command.ExecuteNonQuery();
            command.Dispose();
            
        }

        public int GetStatus(int userId)
        {
            SqlCommand command = new SqlCommand("SELECT Status FROM Student WHERE UserId = @UserId", conn);
            command.Parameters.AddWithValue("@UserId", userId);
            object returnObject = command.ExecuteScalar();
            command.Dispose();
            
            if(returnObject == null) return -1;
            return (Int16) returnObject;
        }

        public bool CheckNationalId(string nationalId)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 1 FROM Student WHERE NationalId = @NationalId", conn);
            command.Parameters.AddWithValue("@NationalId", nationalId);
            object returnObject = command.ExecuteScalar();
            command.Dispose();
            ;
            if (returnObject == null) return false;
            return true;
        }
        public bool CheckPhone(string phoneNumber)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 1 FROM Student WHERE PhoneNumber = @PhoneNumber", conn);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            object returnObject = command.ExecuteScalar();
            command.Dispose();
            ;
            if (returnObject == null) return false;
            return true;
        }

        public List<StudentSummary> GetSummary()
        {
            List<StudentSummary> studentsSummary = new List<StudentSummary>();
            SqlCommand command = new SqlCommand(@";WITH CTE
                                                    AS
                                                    (
                                                        SELECT s.StudentId, s.FirstName, s.LastName, s.Address, s.PhoneNumber, s.DateOfBirth, s.GuardianName, s.NationalId, s.UserId, su.SubjectName, sR.Grade, s.Status
                                                        FROM Student s LEFT JOIN (SubjectResult sR LEFT JOIN Subject su on sR.SubjectId = su.SubjectId) on s.StudentId = sR.StudentId
                                                    )          
                                                    SELECT DISTINCT(i1.StudentId), i1.FirstName, i1.LastName, i1.Address, i1.PhoneNumber, i1.DateOfBirth, i1.GuardianName, i1.NationalId, i1.UserId, STUFF(
                                                               (SELECT
                                                                    ', ' + SubjectName
                                                                    FROM CTE i2
                                                                    WHERE i1.StudentId = i2.StudentId
                                                                    FOR XML PATH(''))
                                                               ,1,2, ''
                                                            ) as SubjectsTaken, SUM(i1.Grade) as TotalResult, i1.Status     
    
                                                    FROM CTE i1
                                                    GROUP BY i1.StudentId, i1.FirstName, i1.LastName, i1.Address, i1.PhoneNumber, i1.DateOfBirth, i1.GuardianName, i1.NationalId, i1.UserId, i1.Status
                                                    ORDER BY i1.Status DESC, TotalResult DESC", conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                studentsSummary.Add(
                    new StudentSummary()
                    {
                        StudentId = (int)reader["StudentId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Address = (string)reader["Address"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        GuardianName = (string)reader["GuardianName"] ?? "",
                        NationalId = (string)reader["NationalId"],
                        UserId = (int)reader["UserId"],
                        SubjectsTaken = (string)reader["SubjectsTaken"],
                        TotalResult = (int)reader["TotalResult"],
                        Status = ((Status)reader["Status"]).ToString(),
                    });
            }
            reader.Close();
            command.Dispose();
            
            return studentsSummary;
        }
    }
}
