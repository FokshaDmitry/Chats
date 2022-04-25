using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class AddDbContext : DbContext
    {
        public DbSet<LibProtocol.Models.Online> Online { get; set; }
        public DbSet<LibProtocol.Models.User> DbUsers { get; set; }
        public DbSet<LibProtocol.Models.Message> Message { get; set; }

        public AddDbContext()
        { 
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Проекты\Chats\Server\DatabaseUsers.mdf;Integrated Security=True");
        }
    }
}
