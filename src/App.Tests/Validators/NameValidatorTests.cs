using App.Models;
using App.Validators;
using NUnit.Framework;

namespace App.Tests.Validators
{
    public class NameValidatorTests
    {
        [TestCase(true, "jack", "asdasdzxczczxcasd")]
        [TestCase(false, "asdzxczx", null)]
        [TestCase(false, null, "asdsad")]
        [TestCase(false, null, null)]
        [TestCase(false, "", null)]
        [TestCase(false, null, "")]
        [TestCase(false, "", "")]
        public void ThenTheValidatorValidatesTheContent(bool valid, string firstname, string surname)
        {
            var name = new Name(firstname, surname);
            var result = new NameValidator().Validate(name);

            Assert.That(result, Is.EqualTo(valid));
        }
    }
}