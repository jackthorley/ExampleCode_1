namespace App.Models
{
    public class Name
    {
        public string FirstName { get; }
        public string Surname { get; }

        public Name(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }
    }
}