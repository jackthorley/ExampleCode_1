using System;

namespace App.Services.Interfaces
{
    public interface ICustomerService
    {
        bool AddCustomer(string firstName, string surname, string emailAddress, DateTime dateOfBirth, int companyId);
    }
}