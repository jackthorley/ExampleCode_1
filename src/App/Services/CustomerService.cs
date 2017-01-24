using System;
using App.Data.Criteria;
using App.Data.Interfaces;
using App.Models;
using App.Services.Interfaces;

namespace App.Services
{
    //Full journey to be covered in acceptance test to monitor behavior
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerValidationService _validator;
        private readonly IQuery<Company, int> _companyRetrievalQuery;
        private readonly ICustomerCreditCalculationService _customerCreditCalculationService;
        private readonly ICommand<CustomerCriteria> _customerCreateCommand;

        //Ideally I'd use a DI framework or use my own service factory infrastructure to localise this creation
        internal CustomerService()
            : this(ServiceFactory.CreateCustomerValidatorService(),
                   ServiceFactory.CreateCompanyRetievalQuery(),
                   ServiceFactory.CreateCustomerCreditCalculationService(),
                   ServiceFactory.CreateCustomerDataAccess())
        {
        }

        internal CustomerService(ICustomerValidationService validator,
                               IQuery<Company, int> companyRetrievalQuery,
                               ICustomerCreditCalculationService customerCreditCalculationService,
                               ICommand<CustomerCriteria> customerCreateCommand)
        {
            _validator = validator;
            _companyRetrievalQuery = companyRetrievalQuery;
            _customerCreditCalculationService = customerCreditCalculationService;
            _customerCreateCommand = customerCreateCommand;
        }

        /* Quite a lot of parameters on this would roll up to an object unless this is external where base types are possibly preferred.
         * Also what does bool mean to the end user? A specific ResultType could be handy.
         * By this point it could be said that the add should be a thin wrapper over the data layer, validation should be done before this layer. */
        public bool AddCustomer(string firstName, string surname, string emailAddress, DateTime dateOfBirth, int companyId)
        {
            var customer = new Customer(firstName, surname, emailAddress, dateOfBirth);

            var status = _validator.Validate(customer);
            if (status != ModelStatus.Valid)
                return false;

            var company = _companyRetrievalQuery.Execute(companyId);
            customer = _customerCreditCalculationService.ApplyCredit(customer, company);

            //More business logic in this function - it's debatable that it should live here or be configurable
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            var customerCriteria = new CustomerCriteria(customer.Firstname,
                                                        customer.Surname,
                                                        customer.DateOfBirth,
                                                        customer.EmailAddress,
                                                        customer.HasCreditLimit,
                                                        customer.CreditLimit,
                                                        companyId);

            _customerCreateCommand.Execute(customerCriteria);

            return true;
        }
    }
}
