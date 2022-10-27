using DAL.Models;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class GradeRepository : DatabaseHelper, IRepository<Grade>
    {
        public int Create(Grade grade)
        {
            int rows = 0;
            SqlCommand command;
            var subjectsResults = grade.SubjectId.Zip(grade.Result, (s, r) => new { Subject = s, Result = r });
            foreach(var subjectResult in subjectsResults)
            {
                command = new SqlCommand("INSERT INTO grade (student_id, subject_id, result) " +
                "VALUES (@StudentId, @SubjectId, @Result); ", conn);
                command.Parameters.AddWithValue("@StudentId", grade.StudentId);
                command.Parameters.AddWithValue("@SubjectId", subjectResult.Subject);
                command.Parameters.AddWithValue("@Result",  (byte) subjectResult.Result);
                rows = command.ExecuteNonQuery();
                command.Dispose();
            }
            return rows;
        }

        public Grade Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Grade> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Grade entity)
        {
            throw new NotImplementedException();
        }
    }
}