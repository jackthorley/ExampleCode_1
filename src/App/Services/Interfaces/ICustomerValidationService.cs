using App.Models;

namespace App.Services.Interfaces
{
    public interface ICustomerValidationService
    {
        ModelStatus Validate(Customer customer);
    }
}