using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using App.Data.Criteria;
using App.Data.Interfaces;

namespace App.Data
{
    /*
     * As this interacts with the database and contains no business logic this can simply be integration tested.
     * Writing unit tests don't provide a huge benefit to the test suite as this unit is interested in the output.
     * 
     * This would be completed by running the tests against a database which is provisioned and versioned.
     */
    public class CustomerDataAccess : ICommand<CustomerCriteria>
    {
        public void Execute(CustomerCriteria customer)
        {
        var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

        using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspAddCustomer"
                };

                var firstNameParameter = new SqlParameter("@Firstname", SqlDbType.VarChar, 50) {Value = customer.Firstname};
                command.Parameters.Add(firstNameParameter);
                var surnameParameter = new SqlParameter("@Surname", SqlDbType.VarChar, 50) {Value = customer.Surname};
                command.Parameters.Add(surnameParameter);
                var dateOfBirthParameter = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) {Value = customer.DateOfBirth};
                command.Parameters.Add(dateOfBirthParameter);
                var emailAddressParameter = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50) {Value = customer.EmailAddress};
                command.Parameters.Add(emailAddressParameter);
                var hasCreditLimitParameter = new SqlParameter("@HasCreditLimit", SqlDbType.Bit) {Value = customer.HasCreditLimit};
                command.Parameters.Add(hasCreditLimitParameter);
                var creditLimitParameter = new SqlParameter("@CreditLimit", SqlDbType.Int) {Value = customer.CreditLimit};
                command.Parameters.Add(creditLimitParameter);
                var companyIdParameter = new SqlParameter("@CompanyId", SqlDbType.Int) {Value = customer.CompanyId};
                command.Parameters.Add(companyIdParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}