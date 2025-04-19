using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sneaker_Ecommerce.Domain.Entity;
using Sneaker_ECommerce.Infrastructure.Data;
using SneakerECommerce.Application.DTOs.Request;
using SneakerECommerce.Application.Interfaces.IRepositories;
using StoreManagement.Application.DTOs.Auth;
using System.Linq.Expressions;

namespace SneakerECommerce.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext, IMapper mapper) 
        { 
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<int> CountAsync(string searchTerm)
        {
            var query = _dataContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Name.Contains(searchTerm));
            }

            return await query.CountAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            var newUser = _mapper.Map<User>(user);
            _dataContext.Users.Add(newUser);
            await _dataContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteAsync(int id, bool incluDeleted = false)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            user.IsDeleted = true;
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var users = _dataContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(t => t.Name.Contains(searchTerm));
            }
            if (!incluDeleted)
            {
                users = users.Where(t => t.IsDeleted == incluDeleted);
            }
            var list = await users.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToListAsync();
            return list;
        }
        public Expression<Func<User, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                case "username":
                    return x => x.Name;
                case "phones":
                    return x => x.Phone;
                case "email":
                    return x => x.Email;
                case "role":
                    return x => x.Role;
                default:
                    return x => x.Id;
            }
        }

        public async Task<User> GetByEmailAsync(string email, bool incluDeleted = false)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.IsDeleted == incluDeleted);
            return user;
        }

        public async Task<User> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            return user;
        }

        public async Task<User> GetByLoginAsync(string email, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x=>x.Email == email && x.Password == password);
            return user;
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdatePasswordAsync(int id, string password, bool incluDeleted = false)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.Password == password && x.IsDeleted == incluDeleted);
            user.Password = password;
            _dataContext.Update(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
