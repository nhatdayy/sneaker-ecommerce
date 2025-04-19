using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerECommerce.Application.Common;
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
        public async Task<ActionResult> Register(RegisterDTO request)
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
        public async Task<ActionResult> Login(LoginDTO request)
        {
            var result = await _authenticationService.Login(request);
            if (result.errors.Any())
            {
                return BadRequest(Result.Failure(result.errors[0]));
            }
            return Ok(Result<AuthResult>.Success(result, "Đăng nhập thành công"));
        }

    }
}
