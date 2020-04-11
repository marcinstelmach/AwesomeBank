namespace AwesomeBank.Identity.Domain.Interfaces
{
    using AwesomeBank.Identity.Domain.ValueObjects;

    public interface IPasswordFactory
    {
        Password Create(string passwordText);
    }
}