namespace StoreManagement.Application.DTOs.Auth
{
    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public int Role { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public List<string> errors {  get; set; } = new List<string>();
        
    }
}
