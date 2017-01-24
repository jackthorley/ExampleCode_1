namespace App.Validators
{
    internal class EmailValidator : IValidator<string>
    {
        public bool Validate(string emailAddress)
        {
            return emailAddress.Contains("@") || emailAddress.Contains(".");
        }
    }
}