using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Course_Work_1.Models
{
    public class AppDbContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationSection section = Configuration.GetSection("ConnectionStrings");
            string conn_str = section["DefaultConnection"];
            optionsBuilder.UseSqlServer(conn_str);
        }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Model> Model { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<GeneralInformation> GeneralInformation { get; set; }
    }
}
