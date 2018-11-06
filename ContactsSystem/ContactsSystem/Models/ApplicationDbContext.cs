using System.Data.Entity;

namespace ContactsSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public ApplicationDbContext()
           : base("Contacts")
        {
        }
    }
}