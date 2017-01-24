using System;
using App.Models;
using App.Services.Interfaces;
using App.Validators;

namespace App.Services
{
    //Needs testing
    internal class CustomerValidationService : ICustomerValidationService
    {
        private readonly IValidator<string> _emailValidator;
        private readonly IValidator<DateTime> _dateOfBirthValidator;
        private readonly IValidator<Name> _nameValidator;

        public CustomerValidationService(IValidator<string> emailValidator, IValidator<DateTime> dateOfBirthValidator, IValidator<Name> nameValidator)
        {
            _emailValidator = emailValidator;
            _dateOfBirthValidator = dateOfBirthValidator;
            _nameValidator = nameValidator;
        }

        //Could return an array of model statuses to ensure user gets all feedback to act on.
        public ModelStatus Validate(Customer customer)
        {
            if(!_emailValidator.Validate(customer.EmailAddress))
                return ModelStatus.InvalidEmail;
            
            if(!_dateOfBirthValidator.Validate(customer.DateOfBirth))
                return ModelStatus.InvalidDateOfBirth;
            
            if(!_nameValidator.Validate(new Name(customer.Firstname, customer.Surname)))
                return  ModelStatus.InvalidName;
            
            return ModelStatus.Valid;
        }
    }
}