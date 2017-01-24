namespace App.Validators
{
    internal interface IValidator<in T>
    {
        bool Validate(T input);
    }
}