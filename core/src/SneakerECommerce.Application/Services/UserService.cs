using Sneaker_Ecommerce.Domain.Entity;
using SneakerECommerce.Application.Common;
using SneakerECommerce.Application.Interfaces.IRepositories;
using SneakerECommerce.Application.Interfaces.IServices;
using StoreManagement.Application.DTOs.Auth;
using AutoMapper;
using SneakerECommerce.Application.DTOs.Request;



namespace SneakerECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository<User> userRepository,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<PaginationResult<List<UserDTO>>> GetAll(string currentPage = "1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _asc = bool.Parse(asc);

            var totalRecords = await _userRepository.CountAsync(searchTerm);
            var list = await _userRepository.GetAll(_currentPage, _pageSize, searchTerm, sortColumn, _asc);
            var count = list.Count();

            var listUser = _mapper.Map<List<UserDTO>>(list);
            return PaginationResult<List<UserDTO>>.Create(listUser, _currentPage, _pageSize, totalRecords);
        }

        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var login = await _userRepository.GetByLoginAsync(loginDTO.Email, loginDTO.Password);
            return _mapper.Map<UserDTO>(login);
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var newUser = _mapper.Map<User>(registerDTO);
            await _userRepository.CreateAsync(newUser);
            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user) ?? null;
        }

        public async Task<UserDTO> GetByEmalAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var edit = await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserDTO>(edit);
        }

        public async Task<UserDTO> UpdatePasswordAsync(int id, string password)
        {
            var user = await _userRepository.UpdatePasswordAsync(id, password);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
