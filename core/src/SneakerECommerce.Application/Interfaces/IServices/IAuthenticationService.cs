using StoreManagement.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Application.Interfaces.IServices
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Login(LoginDTO loginDTO);
        Task<AuthResult> Register(RegisterDTO registerDTO);
        Task<AuthResult> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<AuthResult> ResetPassword(RestorePasswordDTO resetPasswordDTO);
    }
}
