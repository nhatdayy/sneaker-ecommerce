
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Win32;
using SneakerECommerce.Application.Interfaces.IServices;
using StoreManagement.Application.DTOs.Auth;
using System.Net.Mail;
using System.Net;
using SneakerECommerce.Application.DTOs.Auth;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SneakerECommerce.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IJwtManager _jwtManager;
        public AuthenticationService(IJwtManager jwtManager, IUserService userService)
        {
            _jwtManager = jwtManager;
            _userService = userService;
        }
        public async Task<AuthResult> ChangePassword(ChangePasswordDTO request)
        {
            AuthResult result = new AuthResult();
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                result.errors.Add("Vui lòng nhập đầy đủ thông tin");
                return result;
            }
            if (request.OldPassword == request.Password)
            {
                result.errors.Add("Mật khẩu mới trùng với mật khẩu cũ");
                return result;
            }
            if (request.ConfirmPassword != request.Password)
            {
                result.errors.Add("Mật khẩu mới không trùng khớp");
                return result;
            }

            var user = await _userService.GetByEmalAsync(request.Email);
            if (user == null)
            {
                result.errors.Add("Sai thông tin tài khoản");
                return result;
            }
            else
            {
                request.Password = _jwtManager.getHashPassword(request.Password);
                var edit = await _userService.UpdatePasswordAsync(user.Id, request.Password);
                var token = _jwtManager.CreateToken(edit);
                result.Token = token;
                return result;
            }
        }

        public async Task<AuthResult> Login(LoginDTO loginDTO)
        {
            AuthResult result = new AuthResult();
            if(loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                throw new Exception("Vui lòng nhập đầy đủ thông tin");
            } 
            var passHash = _jwtManager.getHashPassword(loginDTO.Password);
            loginDTO.Password = passHash;
            var user = await _userService.LoginAsync(loginDTO);
            if(user == null)
            {
                result.errors.Add("Sai thông tin tài khoản");
                return result;
            }
            else
            {
                var token = _jwtManager.CreateToken(user);
                result.Token = token;
                result.Role = user.Role;
                result.Name = user.Name;
                return result;
            }
        }

        public async Task<AuthResult> Register(RegisterDTO registerDTO)
        {
            AuthResult result = new AuthResult();
            if (registerDTO == null || string.IsNullOrEmpty(registerDTO.Email) || string.IsNullOrEmpty(registerDTO.Password))
            {
                throw new Exception("Vui lòng nhập đầy đủ thông tin");
            }
            var emailExists = await _userService.GetByEmalAsync(registerDTO.Email);
            if (emailExists != null)
            {
                throw new Exception("Email đã tồn tại");
            }
            else
            {
                registerDTO.Password = _jwtManager.getHashPassword(registerDTO.Password);
                var user = await _userService.RegisterAsync(registerDTO);
                var token = _jwtManager.CreateToken(user);
                result.Token = token;
                result.Name = user.Name;
                return result;
            }
        }

        public async Task<bool> ResetPassword(RestorePasswordDTO resetPasswordDTO)
        {
            if(resetPasswordDTO == null || string.IsNullOrEmpty(resetPasswordDTO.Email))
            {
                throw new Exception("Vui lòng nhập đầy đủ thông tin");
            }
            var user = await _userService.GetByEmalAsync(resetPasswordDTO.Email);
            if (user != null) {
                var newPassword = randomPassowrd();
                var pashHash =  _jwtManager.getHashPassword(newPassword);
                var userUpdated = await _userService.UpdatePasswordAsync(user.Id, pashHash);
                if (userUpdated != null)
                {
                    SendEmailAsync(resetPasswordDTO.Email, newPassword);
                    return true;
                }
            }
            throw new Exception("Không thể gửi OTP");
        }
        private string randomPassowrd()
        {
            Random rand = new Random();
            int stringlen = rand.Next(4, 10);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {
                // Generating a random number. 
                randValue = rand.Next(0, 26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                str = str + letter;
            }
            return str;
        }

        public bool SendEmailAsync(string email, string message)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                var to = email;
                var from = "nhat23891@gmail.com";
                var pass = "zohi sncr hcqk kwwd";
                var messgeBody = "Mật khẩu mới của bạn là: " + message;
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(from);
                mailMessage.Body = messgeBody;
                mailMessage.Subject = "Khôi phục mật khẩu tài khoản";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, pass);

                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CheckToken> CheckAccessToken(string token)
        {
            CheckToken result = new CheckToken();
            var principal = _jwtManager.ValidateToken(token);
            if (principal != null)
            {
                result.Role = principal.FindFirst("Roles")?.Value;
                result.IsDenied = false;
            }
            else
            {
                result.IsDenied = true;
            }
            return result;
        }
        
    }
}

