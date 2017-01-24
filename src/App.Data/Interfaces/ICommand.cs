namespace App.Data.Interfaces
{
    public interface ICommand<in T>
    {
        void Execute(T input);
    }
}