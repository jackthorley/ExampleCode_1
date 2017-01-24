using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using App.Data.Interfaces;
using App.Models;

namespace App.Data
{
    //My personal preference is to have the SQL in code rather than in SP's -> means the code controls the DB interactions
    //Less likihood for the sp to get changed externally to a code release hence causing a breaking change
    public class CompanyGetByIdQuery : IQuery<Company, int>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

        public Company Execute(int id)
        {
            Company company = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetCompanyById"
                };

                var parameter = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var companyId = int.Parse(reader["CompanyId"].ToString());
                    var name = reader["Name"].ToString();

                    company = new Company(companyId, name);
                }
            }

            return company;
        }
    }
}
