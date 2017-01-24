using System;

namespace App.Validators
{
    internal class DateOfBirthValidator : IValidator<DateTime>
    {
        public bool Validate(DateTime dateOfBirth)
        {
            return ValidateDoB(dateOfBirth);
        }

        private static bool ValidateDoB(DateTime dateOfBirth)
        {
            //Should use UTC now as all dates should really be normalised incase distributed system
            var now = SystemTime.Now;

            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }
    }
}