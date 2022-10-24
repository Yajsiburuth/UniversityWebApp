using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using UniversityWebApp.Helper;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repositories
{
    public class SubjectRepository : IRepository<Subject>
    {
        private readonly DatabaseHelper databaseHelper = new DatabaseHelper();

        public int Create(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Subject Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subject> GetAll()
        {
            List<Subject> subjects = new List<Subject>();
            using (SqlConnection _conn = databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM subject", _conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var subject = new Subject()
                    {
                        SubjectId = reader.GetInt32(0),
                        SubjectName = reader.GetString(1)
                    };
                    subjects.Add(subject);
                }
                _conn.Close();
            }
            return subjects;
        }

        public void Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
