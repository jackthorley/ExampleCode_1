using App.Data;
using App.Data.Criteria;
using App.Data.Interfaces;
using App.Models;
using App.Services;
using App.Services.Interfaces;
using App.Validators;

namespace App
{
    public static class ServiceFactory
    {
        public static ICustomerValidationService CreateCustomerValidatorService()
        {
            return new CustomerValidationService(new EmailValidator(), new DateOfBirthValidator(), new NameValidator());
        }

        public static IQuery<Company, int> CreateCompanyRetievalQuery()
        {
            return new CompanyGetByIdQuery();
        }

        public static ICustomerCreditCalculationService CreateCustomerCreditCalculationService()
        {
            return new CustomerCreditCalculationService();
        }

        public static ICommand<CustomerCriteria> CreateCustomerDataAccess()
        {
            return new CustomerDataAccess();
        }
    }
}
