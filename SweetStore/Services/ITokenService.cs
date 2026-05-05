using SweetStore.Model;

namespace SweetStore.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
