using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bokningsappen.Models
{
    internal class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mydberik.database.windows.net,1433;Initial Catalog=ErikDatabaseTwo;Persist Security Info=False;User ID=erikadmin;Password=Pumpa123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Weekday> Weekdays { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
