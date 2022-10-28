using DAL.Models;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class SubjectResultRepository : DatabaseHelper, IRepository<SubjectResult>
    {
        public int Create(SubjectResult subjectResult)
        {
            int rows = 0;
            SqlCommand command;
            var subjectsAndResults = subjectResult.SubjectId.Zip(subjectResult.Result, (s, r) => new { Subject = s, Result = r });
            foreach(var subjectAndResult in subjectsAndResults)
            {
                command = new SqlCommand(@"INSERT INTO SubjectResult (StudentId, SubjectId, Grade)
                VALUES (@StudentId, @SubjectId, @Grade); ", conn);
                command.Parameters.AddWithValue("@StudentId", subjectResult.StudentId);
                command.Parameters.AddWithValue("@SubjectId", subjectAndResult.Subject);
                command.Parameters.AddWithValue("@Grade",  (byte)subjectAndResult.Result);
                rows = command.ExecuteNonQuery();
                command.Dispose();
            }
            return rows;
        }

        public SubjectResult Find(int subjectResultId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SubjectResult entity)
        {
            throw new NotImplementedException();
        }
    }
}