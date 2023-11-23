namespace BiznewWebUI.Models
{
    public class Advort:BaseEntity
    {
        public string AdvortName { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public int viewCount { get; set; }
        public bool IsPublished { get; set; }
      public AdvortsArticle? AdvortsArticles { get; set; }

    }
}
