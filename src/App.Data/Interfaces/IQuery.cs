namespace App.Data.Interfaces
{
    public interface IQuery<out TOutput, in TInput>
    {
        TOutput Execute(TInput input);
    }
}