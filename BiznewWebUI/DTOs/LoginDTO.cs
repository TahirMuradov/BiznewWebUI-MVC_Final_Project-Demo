using System.ComponentModel.DataAnnotations;

namespace BiznewWebUI.DTOs
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
        public bool RememverMe { get; set; }
    }
}
