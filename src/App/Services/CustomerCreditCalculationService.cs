using App.Models;
using App.Services.Interfaces;

namespace App.Services
{
    internal class CustomerCreditCalculationService : ICustomerCreditCalculationService
    {
        public Customer ApplyCredit(Customer customer, Company company)
        {
            var hasCreditLimit = true;
            var totalCreditLimit = 0;

            /*
             * Having this 'name==' logic in here is 'ok'.
             * The value should live in the DB as it's likely to be set by another system (CRM?).
             * What happens if a new 'Important' customer gets added - you have all to deal with re-releasing the code to ensure
             * the creditLimit is maintainable.
             * Also if the business rules around what an Important client get's changes, this would require a full code release.
             */
            if (company.Name == "VeryImportantClient")
            {
                hasCreditLimit = false;
            }
            else if (company.Name == "ImportantClient")
            {
                //No need for original commends now - reads much easier. Should really self describe
                var creditLimit = RetrieveCreditLimit(customer);
                totalCreditLimit = creditLimit * 2;
            }
            else
            {
                totalCreditLimit = RetrieveCreditLimit(customer);
            }

            return new Customer(customer.Firstname,
                                customer.Surname,
                                customer.EmailAddress,
                                customer.DateOfBirth,
                                new CustomerCredit(hasCreditLimit, totalCreditLimit));
        }

        private static int RetrieveCreditLimit(Customer customer)
        {
            /* Want to keep connection time to it's lowest - remove all code that can be executed after dispose.
               This should be abstracted away so the Client can be swapped out without changing the implementation of this function. */
            using (var customerCreditService = new CustomerCreditServiceClient())
            {
                return customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
            }
        }
    }
}