using System.Security;

namespace TestingADLogging
{
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
