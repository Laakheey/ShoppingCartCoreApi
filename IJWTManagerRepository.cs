using Microsoft.Win32;

namespace Jwt.Model
{
    public interface IJWTManagerRepository
    {

        Task<LoginResponseModel> Login(Login model);

        Task<LoginResponseModel> SignUp(Register model);
      
    }
}
