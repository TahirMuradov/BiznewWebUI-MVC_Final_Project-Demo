using BiznewWebUI.Models;

namespace BiznewWebUI.Areas.Dashboard.ViewModels
{
    public class UserRoleVM
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
