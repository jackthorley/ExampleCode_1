using App.Validators;
using NUnit.Framework;

namespace App.Tests.Validators
{
    [TestFixture("validemail@valid.com", true)]
    [TestFixture("valid.email", true)]
    [TestFixture("valid@email", true)]
    [TestFixture("invalidemailinvalidcom", false)]
    public class EmailValidatorTests
    {
        private readonly string _emailAddress;
        private readonly bool _isValid;
        private bool _result;

        public EmailValidatorTests(string emailAddress, bool isValid)
        {
            _emailAddress = emailAddress;
            _isValid = isValid;
        }

        [OneTimeSetUp]
        public void GivenAnEmail()
        {
            _result = new EmailValidator().Validate(_emailAddress);
        }

        [Test]
        public void ThenTheValidatorValidatesTheContent()
        {
            Assert.That(_result, Is.EqualTo(_isValid));
        }
    }
}
