namespace BiznewWebUI.Models
{
    public class BaseEntity
    {

        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeletedDate { get; set; }

    }
}
