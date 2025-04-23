using SneakerECommerce.Application.Common;
using SneakerECommerce.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> GetByEmalAsync(string email);
        Task<bool> DeleteAsync(int id);
        Task<UserDTO> UpdateAsync(UserDTO userDTO);
        Task<UserDTO> UpdatePasswordAsync(int id, string password);
        Task<PaginationResult<List<UserDTO>>> GetAll(string currentPage, string pageSize, string searchTerm, string sortColumn, string asc);
    }
}
