using BiznewWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BiznewWebUI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<LeaveComment> LeaveComments { get; set; }
        public DbSet<ArticleComments> ArticleComments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ContactUs> ContactUsMessages { get; set; }
         public DbSet<Advort> Advorts { get; set; }
        public DbSet<AdvortsArticle> AdvortsArticles { get; set; }
        public DbSet<UserActions> UserActions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<ArticleComments>()
                       .HasOne(a => a.User)
                       .WithMany(b => b.ArticleComments)
                       .HasForeignKey(c => c.UserId)
                       .OnDelete(DeleteBehavior.Restrict);

          

            builder.Entity<ArticleTag>()
       .HasOne(a => a.Tags)
       .WithMany(b => b.ArticleTag)
       .HasForeignKey(c => c.TagId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
      .HasOne(c => c.User)
      .WithMany(c=> c.Categories)
      .HasForeignKey(z => z.UserId)
      .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tag>()
   .HasOne(c => c.User)
   .WithMany(b=>b.Tags)
   .HasForeignKey(z => z.UserId)
   .OnDelete(DeleteBehavior.Restrict);

     

        }


    }
}
