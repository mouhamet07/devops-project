using gestionStock.Models;
namespace gestionStock.Services
{
    public interface IAuthService
    {
        User Login(LoginVM model);
        void Logout();
    }
}