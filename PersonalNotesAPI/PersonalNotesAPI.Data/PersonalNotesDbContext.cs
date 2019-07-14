using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalNotesAPI.Model.Models;

namespace PersonalNotesAPI.Data
{
    public class PersonalNotesDbContext : IdentityDbContext<ApplicationUser>
    {
        public PersonalNotesDbContext(DbContextOptions<PersonalNotesDbContext> options): base(options)
        {

        } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
    }
}
