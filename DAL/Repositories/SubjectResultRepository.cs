﻿using DAL.Models;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repositories
{
    public class SubjectResultRepository : DatabaseHelper, IRepository<SubjectResult>, ISubjectResultRepository
    {
        public int Create(SubjectResult subjectResult)
        {
            int rows = 0;
            SqlCommand command;
            var subjectsAndResults = subjectResult.SubjectId.Zip(subjectResult.Result, (s, r) => new { Subject = s, Result = r });
            foreach (var subjectAndResult in subjectsAndResults)
            {
                command = new SqlCommand(@"INSERT INTO SubjectResult (StudentId, SubjectId, Grade)
                VALUES (@StudentId, @SubjectId, @Grade); ", conn);
                command.Parameters.AddWithValue("@StudentId", subjectResult.StudentId);
                command.Parameters.AddWithValue("@SubjectId", subjectAndResult.Subject);
                command.Parameters.AddWithValue("@Grade", (byte)subjectAndResult.Result);
                rows = command.ExecuteNonQuery();
                command.Dispose();
            }
            return rows;
        }

        public void CreateResults(SubjectResult subjectResult)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[3] { new DataColumn("StudentId", typeof(int)),
                                        new DataColumn("SubjectId", typeof(Int16)),
                                        new DataColumn("Grade", typeof(byte))});
            var subjectsAndResults = subjectResult.SubjectId.Zip(subjectResult.Result, (s, r) => new { Subject = s, Result = r });
            foreach(var subjectAndResult in subjectsAndResults)
                dataTable.Rows.Add(subjectResult.StudentId, subjectAndResult.Subject, (byte)subjectAndResult.Result);

            SqlTransaction transaction = conn.BeginTransaction();
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers, transaction);
            sqlBulkCopy.DestinationTableName = "SubjectResult";
            sqlBulkCopy.ColumnMappings.Add("StudentId", "StudentId");
            sqlBulkCopy.ColumnMappings.Add("SubjectId", "SubjectId");
            sqlBulkCopy.ColumnMappings.Add("Grade", "Grade");
            try { 
                sqlBulkCopy.WriteToServer(dataTable);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }


        public SubjectResult Find(int studentId)
        {
            SubjectResult subjectResult = new SubjectResult();
            subjectResult.StudentId = studentId;
            SqlCommand command = new SqlCommand("SELECT SubjectResultId, SubjectId, Grade FROM SubjectResult WHERE StudentId = @StudentId", conn);
            command.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subjectResult.SubjectResultId.Add(reader.GetInt32(0));
                subjectResult.SubjectId.Add(reader.GetInt16(1));
                subjectResult.Result.Add((Grade)reader.GetByte(2));
            }
            reader.Close();
            command.Dispose();
            return subjectResult;
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