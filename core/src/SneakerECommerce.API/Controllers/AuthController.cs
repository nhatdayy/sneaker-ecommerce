using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerECommerce.Application.Common;
using SneakerECommerce.Application.DTOs.Auth;
using SneakerECommerce.Application.DTOs.Request;
using SneakerECommerce.Application.Interfaces.IServices;
using StoreManagement.Application.DTOs.Auth;

namespace SneakerECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtManager _jwtManager;

        public AuthController(IAuthenticationService authenticationService, IJwtManager jwtManager)
        {
            _authenticationService = authenticationService;
            _jwtManager = jwtManager;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> Register(RegisterDTO request)
        {
            var result = await _authenticationService.Register(request);
            if (result.errors.Any())
            {
                return BadRequest(Result<AuthResult>.Failure(result.errors[0]));
            }
            return Ok(Result<AuthResult>.Success(result, "Đăng ký thành công"));
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> Login(LoginDTO request)
        {
            var result = await _authenticationService.Login(request);
            if (result.errors.Any())
            {
                return BadRequest(Result.Failure(result.errors[0]));
            }
            return Ok(Result<AuthResult>.Success(result, "Đăng nhập thành công"));
        }
        [HttpPost("refresh-token")]
        public ActionResult RefreshToken(UserDTO userDTO)
        {
            var token = _jwtManager.CreateToken(userDTO);
            if (token == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                token
            });
        }

        [HttpPost("check-access-token")]
        public async Task<ActionResult<Result>> CheckAccessToken([FromBody] TokenDTO req)
        {
            if (req == null)
            {
                return BadRequest(Result<CheckToken>.Failure("lấy thông tin thất bại"));
            }
            var result = await _authenticationService.CheckAccessToken(req.Token);
            if (result == null)
            {
                return BadRequest(Result<CheckToken>.Failure("lấy thông tin thất bại"));
            }
            return Ok(Result<CheckToken>.Success(result, "Lấy thông tin thành công"));
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(RestorePasswordDTO restorePasswordDTO)
        {
            var result = await _authenticationService.ResetPassword(restorePasswordDTO);
            return Ok(Result<AuthResult>.Success(result, "Gửi mail thành công"));
        }
    }
}
