using App.Models;

namespace App.Services.Interfaces
{
    public interface ICustomerCreditCalculationService
    {
        Customer ApplyCredit(Customer customer, Company company);
    }
}