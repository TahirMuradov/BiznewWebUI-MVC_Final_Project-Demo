using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace BiznewWebUI.DTOs
{
    public class UserEditDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword))]
        public string? NewConfirmPassword { get; set; }
        public string UserName { get; set; }

  
    }
}
