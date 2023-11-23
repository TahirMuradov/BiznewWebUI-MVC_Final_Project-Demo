using BiznewWebUI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiznewWebUI.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
      
        public string Password { get; set; }
        [Compare (nameof(Password))]
        public string ConfirmPassword { get; set; }
    
        public string CreatingUserName (string FirstName,string LastName)
        {
            string userName = LastName + FirstName;
           
            for (int i = 0; i < 6; i++)
            {
                Random random = new Random();
                userName += random.Next(10);

            }
            return userName;
        }
    }
}
