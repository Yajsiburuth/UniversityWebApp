using DAL.Models;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class SubjectRepository : DatabaseHelper, IRepository<Subject>, ISubjectRepository
    {

        public int Create(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Subject Find(int subjectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subject> GetAll()
        {
            List<Subject> subjects = new List<Subject>();
            SqlCommand command = new SqlCommand("SELECT SubjectId, SubjectName FROM [Subject]", conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var subject = new Subject()
                {
                    SubjectId = reader.GetInt16(0),
                    SubjectName = reader.GetString(1)
                };
                subjects.Add(subject);
            }
            reader.Close();
            command.Dispose();
            return subjects;
        }

        public void Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
