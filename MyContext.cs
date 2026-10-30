using ZalohovacServer.Models; 
using Microsoft.EntityFrameworkCore; 

namespace ZalohovacServer
{
    public class MyContext : DbContext
    {
        
        public DbSet<ConfigData> ConfigData { get; set; }

        

        public DbSet<Targets> Targets { get; set; }

        public DbSet<Sources> Sources { get; set; }

        public DbSet<ConfigDataTarget> ConfigDataTargets_1 { get; set; }

        public DbSet<ConfigDataSources> ConfigDataSources_1 { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<ClientsConfigData> ClientsConfigData { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3a1_woloszczukvit_db1;user=woloszczukvit;password=123456;SslMode=none");
        }

       


    }
}
