using Microsoft.EntityFrameworkCore;
using BuyerWebForm.Models;

namespace BuyerWebForm.Data
{
    public class WebFormContext : DbContext
    {
        public string AtlasURI { get; set; }
        public WebFormContext (DbContextOptions<WebFormContext> options)
            : base(options)
        {
        }

        public WebFormContext()
        {

        }

        public virtual DbSet<WebFormFields> WebFormFields { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WebFormFields>();
        }
    }
}
