using System;
using App.Validators;
using NUnit.Framework;

namespace App.Tests.Validators
{
    [TestFixture]
    public class DateOfBirthValidatorForBirthdayOver21Tests
    {
        private bool _result;
        
        [OneTimeSetUp]
        public void Given()
        {
            var now = DateTime.Now;

            SystemTime.NowProvider = () => now;
            _result = new DateOfBirthValidator().Validate(now.AddYears(-21));
        }
      
        [Test]
        public void ThenTheValidatorValidatesTheContent()
        {
            Assert.That(_result, Is.True);
        }
    }

    [TestFixture]
    public class DateOfBirthValidatorForBirthdayUnder21Tests
    {
        private bool _result;

        [OneTimeSetUp]
        public void GivenABirthDayOver21()
        {
            var now = DateTime.Now;

            SystemTime.NowProvider = () => now;
            _result = new DateOfBirthValidator().Validate(now.AddYears(-20));
        }

        [Test]
        public void ThenTheValidatorValidatesTheContent()
        {
            Assert.That(_result, Is.False);
        }
    }
}