using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedTechnologies.Domain.Entities;

namespace RedTechnologies.Infra.Data.Context
{
    public class BaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public BaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            if (connectionString != null)
                options.UseMySql(connectionString, new MySqlServerVersion(new System.Version(8, 0, 11)));
        }

        public DbSet<Order> Order { get; set; }
    }
}
