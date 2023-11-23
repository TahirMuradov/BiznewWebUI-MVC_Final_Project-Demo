namespace BiznewWebUI.Models
{
    public class UserActions
    {
        public Guid Id { get; set; }
        public string ActionName { get; set; }
        public DateTime DateTime { get; set; }
    
        public Article? Article { get; set; }

        public Tag? Tag { get; set; }
    
        public Category? Category { get; set; }
    
        public Advort? Advort { get; set; }
        public bool IsActive { get; set; }= false;
        public string userId { get; set; }
        public User User { get; set; }
     
    }
}
