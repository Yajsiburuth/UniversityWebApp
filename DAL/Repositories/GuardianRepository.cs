using DAL.Models;
using Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly DatabaseHelper _databaseHelper;
        public GuardianRepository() { _databaseHelper = new DatabaseHelper(); }

        public int Create(Guardian guardian)
        {
            int guardianId = 0;
            using (SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO guardian (first_name, last_name, phone_number) " +
                    "VALUES (@FirstName, @LastName, @PhoneNumber); " +
                    "SELECT SCOPE_IDENTITY();"
                    , _conn);
                command.Parameters.AddWithValue("@FirstName", guardian.FirstName);
                command.Parameters.AddWithValue("@LastName", guardian.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", guardian.PhoneNumber);

                object returnObj = command.ExecuteScalar();
                if (returnObj != null)
                {
                    guardianId = int.Parse(returnObj.ToString());
                }
                _conn.Close();
            }
            return guardianId;
        }

        public Guardian Find(int id)
        {
            throw new NotImplementedException();
        }

        public int Find(string phoneNumber)
        {
            int guardianId = 0;
            using(SqlConnection _conn = _databaseHelper.CreateConnection())
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("SELECT id FROM guardian WHERE phone_number=@PhoneNumber", _conn);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                object returnObj = command.ExecuteScalar();
                if (returnObj != null)
                    guardianId = int.Parse(returnObj.ToString());

                _conn.Close();
            }
            return guardianId;
        }

        public IEnumerable<Guardian> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Guardian entity)
        {
            throw new NotImplementedException();
        }
    }
}