using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerECommerce.Application.Common;
using SneakerECommerce.Application.DTOs.Request;
using SneakerECommerce.Application.Interfaces.ICachingService;
using SneakerECommerce.Application.Interfaces.IServices;

namespace SneakerECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICachingService _cachingService;
        public UserController(IUserService userService, ICachingService cachingService)
        {
            _userService = userService;
            _cachingService = cachingService;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result>> GetUserById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result == null)
            {
                return BadRequest(Result.Failure("Không tìm  thấy người dùng"));
            }
            return Ok(Result<UserDTO?>.Success(result,"Lấy thông tin thành công"));
        }
        [HttpGet]
        public async Task<ActionResult<Result>> GetAllUserAsync(string currentPage ="1", string pageSize = "5", string searchTerm = "", string sortColumn = "", string asc = "true")
        {
            var cacheKey = $"users:{currentPage}:{pageSize}:{searchTerm}:{sortColumn}:{asc}";

            // Check cache
            var cachedData = _cachingService.GetData<PaginationResult<List<UserDTO>>>(cacheKey);
            if (cachedData != null)
            {
                return Ok(Result<PaginationResult<List<UserDTO>>>.Success(cachedData, "Lấy từ cache thành công"));
            }

            // Nếu không có cache thì gọi service
            var result = await _userService.GetAll(currentPage, pageSize, searchTerm, sortColumn, asc);
            if (result == null || result.List.Count == 0)
            {
                return BadRequest(Result.Failure("Không tìm thấy người dùng"));
            }

            
            _cachingService.SetData(cacheKey, result);

            return Ok(Result<PaginationResult<List<UserDTO>>>.Success(result, "Lấy thông tin thành công"));
        }
    }
}
