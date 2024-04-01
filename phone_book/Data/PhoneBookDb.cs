using Microsoft.EntityFrameworkCore;
using phone_book.Models;

namespace phone_book.Data
{
    public class PhoneBookDb : DbContext
    {
        public PhoneBookDb (DbContextOptions<PhoneBookDb> options): base(options) { 
        
        }

        public DbSet<User> User => Set<User>();
        public DbSet<Contact> Contact => Set<Contact>();
        public DbSet<ContactType> ContactType => Set<ContactType>();
        public DbSet<AdditionalFields> AdditionalFields => Set<AdditionalFields>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            //ContactType Properties
            modelBuilder.Entity<ContactType>().HasIndex(c => c.TypeName).IsUnique();

            //User Properties
            modelBuilder.Entity<User>().HasIndex(c => c.UserName).IsUnique();





        }

    }
}
