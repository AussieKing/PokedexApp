using Microsoft.EntityFrameworkCore;
using PokedexApp.Models;

namespace PokedexApp.Data
{
    public class PokedexContext : DbContext
    {
        public PokedexContext(DbContextOptions<PokedexContext> options)
            : base(options) { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }
        public DbSet<TypeInfo> TypeInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Pokemon>()
                .HasMany(p => p.Types)
                .WithOne()
                .HasForeignKey(pt => pt.PokemonId);

            modelBuilder.Entity<PokemonType>().HasKey(pt => pt.Id);

            modelBuilder.Entity<TypeInfo>().HasKey(ti => ti.Id);
        }
    }
}
