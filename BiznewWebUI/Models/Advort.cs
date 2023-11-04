namespace BiznewWebUI.Models
{
    public class Advort:BaseEntity
    {
        public string Link { get; set; }
        public string Image { get; set; }
      public AdvortsArticle AdvortsArticles { get; set; }

    }
}
