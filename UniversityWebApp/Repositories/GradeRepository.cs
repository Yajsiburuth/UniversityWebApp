﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityWebApp.Helper;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repositories
{
    public class GradeRepository : IRepository<Grade>
    {
        private readonly DatabaseHelper _databaseHelper = new DatabaseHelper();

        public GradeRepository()
        {

        }
        public int Create(Grade grade)
        {
            int rows = 1;
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command;
                var subjectsResults = grade.SubjectId.Zip(grade.Result, (s, r) => new { Subject = s, Result = r });
                foreach(var subjectResult in subjectsResults)
                {
                    command = new SqlCommand("INSERT INTO grade (student_id, subject_id, result) " +
                    "VALUES (@StudentId, @SubjectId, @Result); ", _conn);
                    command.Parameters.AddWithValue("@StudentId", grade.StudentId);
                    command.Parameters.AddWithValue("@SubjectId", subjectResult.Subject);
                    command.Parameters.AddWithValue("@Result", subjectResult.Result.ToString());
                    rows = command.ExecuteNonQuery();
                }
                _conn.Close();
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