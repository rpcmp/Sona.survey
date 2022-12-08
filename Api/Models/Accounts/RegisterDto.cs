using System.ComponentModel.DataAnnotations;

namespace Api.Models.Accounts
{
    public class RegisterDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
