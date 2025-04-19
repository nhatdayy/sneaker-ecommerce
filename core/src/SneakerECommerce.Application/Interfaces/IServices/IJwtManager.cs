using SneakerECommerce.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Application.Interfaces.IServices
{
    public interface IJwtManager
    {
        string CreateToken(UserDTO userDTO);
        string getHashPassword(string password);
        ClaimsPrincipal ValidateToken(string token);
    }
}
