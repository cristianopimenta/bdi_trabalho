using BDI.TrabalhoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BDI.TrabalhoFinal.Data
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<ContaBancaria> ContasBancarias { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<MotoristaVeiculo> MotoristaVeiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Viagem>()
                .Property(v => v.ValorPagar)
                .HasColumnType("decimal(18, 2)"); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
