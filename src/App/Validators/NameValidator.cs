using App.Models;

namespace App.Validators
{
    internal class NameValidator : IValidator<Name>
    {
        public bool Validate(Name name)
        {
            return !string.IsNullOrEmpty(name.FirstName) && !string.IsNullOrEmpty(name.Surname);
        }
    }
}