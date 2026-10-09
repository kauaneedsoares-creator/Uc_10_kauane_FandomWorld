using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Uc_10_kauane_FandomWorld.Models;

namespace Uc_10_kauane_FandomWorld.Data
{
    public class Uc_10_kauane_FandomWorldContext : DbContext
    {
        public Uc_10_kauane_FandomWorldContext (DbContextOptions<Uc_10_kauane_FandomWorldContext> options)
            : base(options)
        {
        }

        public DbSet<Uc_10_kauane_FandomWorld.Models.Fandom> Fandom { get; set; } = default!;
        public DbSet<Uc_10_kauane_FandomWorld.Models.Fanfic> Fanfic { get; set; } = default!;
        public DbSet<Uc_10_kauane_FandomWorld.Models.Livro> Livro { get; set; } = default!;
        public DbSet<Uc_10_kauane_FandomWorld.Models.Personagem> Personagem { get; set; } = default!;
        public DbSet<Uc_10_kauane_FandomWorld.Models.Plataforma> Plataforma { get; set; } = default!;
        public DbSet<Uc_10_kauane_FandomWorld.Models.Usuario> Usuario { get; set; } = default!;
    }
}
