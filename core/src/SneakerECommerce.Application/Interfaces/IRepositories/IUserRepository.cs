using Sneaker_Ecommerce.Domain.Entity;
using StoreManagement.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Application.Interfaces.IRepositories
{
    public interface IUserRepository<TUser> where TUser : User
    {
        Task<TUser> CreateAsync(TUser user);
        Task<TUser> DeleteAsync(int id, bool incluDeleted = false);
        Task<TUser> GetByIdAsync(int id, bool incluDeleted = false);
        Task<TUser> GetByEmailAsync(string email, bool incluDeleted = false);
        Task<TUser> UpdateAsync(TUser user);
        Task<TUser> UpdatePasswordAsync(int id, string password, bool incluDeleted = false);
        Task<IEnumerable<TUser>> GetAll(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false);
        Task<TUser> GetByLoginAsync(string email,string password);
        Task<int> CountAsync(string searchTerm);
    }
}
