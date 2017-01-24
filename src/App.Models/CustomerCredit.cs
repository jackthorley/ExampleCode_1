namespace App.Models
{
    public class CustomerCredit 
    {
        public bool HasCreditLimit { get; }
        public int CreditLimit { get; }

        public CustomerCredit(bool hasCreditLimit, int creditLimit)
        {
            HasCreditLimit = hasCreditLimit;
            CreditLimit = creditLimit;
        }
    }
}