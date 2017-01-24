using System;

namespace App.Models
{
    //Immutable
    public class Customer
    {
        public string Firstname { get; private set; }

        public string Surname { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string EmailAddress { get; private set; }

        public bool HasCreditLimit { get; private set; }

        public int CreditLimit { get; private set; }

        public Customer(string firstName, string surname, string emailAddress, DateTime dateOfBirth)
        {
            Firstname = firstName;
            Surname = surname;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;
        }

        public Customer(string firstName, string surname, string emailAddress, DateTime dateOfBirth, CustomerCredit customerCredit)
        {
            Firstname = firstName;
            Surname = surname;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;
            HasCreditLimit = customerCredit.HasCreditLimit;
            CreditLimit = customerCredit.CreditLimit;
        }
    }
}