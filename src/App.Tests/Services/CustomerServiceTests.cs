using System;
using App.Data.Criteria;
using App.Data.Interfaces;
using App.Models;
using App.Services;
using App.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace App.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private string _firstName;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;
        private int _companyId;
        private Company _company;
        private Customer _customer;

        private Mock<ICustomerValidationService> _customerDetailValidator;
        private Mock<IQuery<Company, int>> _companyRetrievalQuery;
        private Mock<ICustomerCreditCalculationService> _customerCreditRetrievalService;
        private Mock<ICommand<CustomerCriteria>> _customerCreateCommand;
        private bool _result;

        [SetUp]
        public void GivenValidCustomerWithValidCreditLimits()
        {
            _firstName = "asdasd";
            _surname = "asdasdzxc";
            _email = "asdasnzxczkc";
            _dateOfBirth = DateTime.UtcNow;
            _companyId = 1;

            _company = new Company(_companyId, "Yolo");
            _customer = new Customer(_firstName, _surname, _email, _dateOfBirth, new CustomerCredit(true, 1000));

            _customerDetailValidator = new Mock<ICustomerValidationService>();
            _customerDetailValidator
                .Setup(v => v.Validate(It.IsAny<Customer>()))
                .Returns(ModelStatus.Valid);

            _companyRetrievalQuery = new Mock<IQuery<Company, int>>();
            _companyRetrievalQuery
                .Setup(q => q.Execute(It.IsAny<int>()))
                .Returns(_company);

            _customerCreditRetrievalService = new Mock<ICustomerCreditCalculationService>();
            _customerCreditRetrievalService
                .Setup(s => s.ApplyCredit(It.IsAny<Customer>(), It.IsAny<Company>()))
                .Returns(_customer);

            _customerCreateCommand = new Mock<ICommand<CustomerCriteria>>();

            var customerService = new CustomerService(_customerDetailValidator.Object, _companyRetrievalQuery.Object, _customerCreditRetrievalService.Object, _customerCreateCommand.Object);
            _result = customerService.AddCustomer(_firstName, _surname, _email, _dateOfBirth, _companyId);
        }

        [Test]
        public void ThenTheCustomerValidationIsPerformed()
        {
            _customerDetailValidator.Verify(v => v.Validate(It.Is<Customer>(c => c.Firstname == _firstName &&
                                                                                 c.Surname == _surname &&
                                                                                 c.EmailAddress == _email &&
                                                                                 c.DateOfBirth == _dateOfBirth)));
        }

        [Test]
        public void ThenTheCompanyIsRetrieved()
        {
            _companyRetrievalQuery.Verify(q => q.Execute(_companyId));
        }

        [Test]
        public void ThenTheCompaniesCreditLimitIsSet()
        {
            _customerCreditRetrievalService.Verify(s => s.ApplyCredit(It.Is<Customer>(c => c.Firstname == _firstName &&
                                                                                           c.Surname == _surname &&
                                                                                           c.EmailAddress == _email &&
                                                                                           c.DateOfBirth == _dateOfBirth), _company));
        }

        [Test]
        public void ThenTheCustomerIsPersisted()
        {
            _customerCreateCommand.Verify(c => c.Execute(It.Is<CustomerCriteria>(cc => cc.Firstname == _customer.Firstname &&
                                                                                       cc.Surname == _customer.Surname &&
                                                                                       cc.EmailAddress == _customer.EmailAddress &&
                                                                                       cc.DateOfBirth == _customer.DateOfBirth &&
                                                                                       cc.CreditLimit == _customer.CreditLimit &&
                                                                                       cc.HasCreditLimit == _customer.HasCreditLimit &&
                                                                                       cc.CompanyId == _companyId)));
        }

        [Test]
        public void ThenTheResultIsSuccessFul()
        {
            Assert.That(_result, Is.EqualTo(true));
        }
    }

    [TestFixture(ModelStatus.InvalidDateOfBirth)]
    [TestFixture(ModelStatus.InvalidEmail)]
    [TestFixture(ModelStatus.InvalidName)]
    public class CustomerServiceTestsWithInvalidCustomerDetails
    {
        private readonly ModelStatus _status;
        private string _firstName;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;
        private int _companyId;

        private Mock<ICustomerValidationService> _customerDetailValidator;
        private bool _result;

        public CustomerServiceTestsWithInvalidCustomerDetails(ModelStatus status)
        {
            _status = status;
        }

        [SetUp]
        public void GivenValidCustomerWithValidCreditLimits()
        {
            _firstName = "asdasd";
            _surname = "asdasdzxc";
            _email = "asdasnzxczkc";
            _dateOfBirth = DateTime.UtcNow;
            _companyId = 1;

            _customerDetailValidator = new Mock<ICustomerValidationService>();
            _customerDetailValidator
                .Setup(v => v.Validate(It.IsAny<Customer>()))
                .Returns(_status);

            var customerService = new CustomerService(_customerDetailValidator.Object, Mock.Of<IQuery<Company, int>>(), Mock.Of<ICustomerCreditCalculationService>(), Mock.Of<ICommand<CustomerCriteria>>());
            _result = customerService.AddCustomer(_firstName, _surname, _email, _dateOfBirth, _companyId);
        }

        [Test]
        public void ThenTheCustomerValidationIsPerformed()
        {
            _customerDetailValidator.Verify(v => v.Validate(It.Is<Customer>(c => c.Firstname == _firstName &&
                                                                                 c.Surname == _surname &&
                                                                                 c.EmailAddress == _email &&
                                                                                 c.DateOfBirth == _dateOfBirth)));
        }

        [Test]
        public void ThenTheResultIsFalse()
        {
            Assert.That(_result, Is.EqualTo(false));
        }
    }
}