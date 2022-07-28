using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpBancarias.Data.Models;


namespace OpBancarias.Data
{
    public partial class OpBancariasContext : DbContext
    {
        private const string _connectionStringName = "OpBancariasContext";
        private readonly string _connectionStringValue;

        public OpBancariasContext(IConfiguration appConfig)
        {
            _connectionStringValue = appConfig.GetConnectionString(_connectionStringName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStringValue);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().ToTable("Personas");

            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            //User name, User identification and account´s number should not be duplicated
            modelBuilder.Entity<Cliente>()
                .HasIndex(cl => cl.UserName)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(user => user.UserName)
                .IsUnique();

            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.Identificacion)
                .IsUnique();

            modelBuilder.Entity<Cuenta>()
                .HasIndex(cta => cta.Numero)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
