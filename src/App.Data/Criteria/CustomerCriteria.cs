using System;

namespace App.Data.Criteria
{
    public class CustomerCriteria
    {
        public string Firstname { get; }
        public string Surname { get; }
        public DateTime DateOfBirth { get; }
        public string EmailAddress { get; }
        public bool HasCreditLimit { get; }
        public int CreditLimit { get; }
        public int CompanyId { get; }

        public CustomerCriteria(string firstname, string surname, DateTime dateOfBirth, string emailAddress, bool hasCreditLimit, int creditLimit, int companyId)
        {
            Firstname = firstname;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            HasCreditLimit = hasCreditLimit;
            CreditLimit = creditLimit;
            CompanyId = companyId;
        }
    }
}