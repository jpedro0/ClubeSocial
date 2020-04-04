using ClubeSocial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Context
{
    public class ClubeDBContext: IdentityDbContext
    {
        public ClubeDBContext(DbContextOptions<ClubeDBContext> options): base(options)
        {}

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Clube> Clubes { get; set; }
        public DbSet<Dependente> Dependetes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Mensalidade> Mensalidades { get; set; }
        public DbSet<Socio> Socios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Candidato>().ToTable("Candidatos", "ClubeDB");
            builder.Entity<Candidato>().HasKey(p => p.ClubeId);
            builder.Entity<Candidato>().Property(p => p.Nome).IsRequired();
            builder.Entity<Candidato>().Property(p => p.DataNacimento).IsRequired();
            builder.Entity<Candidato>().HasOne(p => p.Clube).WithMany(p => p.Candidatos);

            builder.Entity<Cartao>().ToTable("Cartoes", "ClubeDB");
            builder.Entity<Cartao>().HasKey(p => p.NumeroDoCartao);
            builder.Entity<Cartao>().Property(p => p.Nome).IsRequired();
            builder.Entity<Cartao>().Property(p => p.ClubeId).IsRequired();
            builder.Entity<Cartao>().Property(p => p.DataVencimento).IsRequired();
            builder.Entity<Cartao>().Property(p => p.Valido).IsRequired();
            builder.Entity<Cartao>().HasOne(p => p.Socio).WithOne(p => p.Cartao);
            builder.Entity<Cartao>().HasOne(p => p.Clube).WithMany(p => p.Cartaos);

            builder.Entity<Clube>().ToTable("Clubes", "ClubeDB");
            builder.Entity<Clube>().HasKey(p => p.ClubeId);
            builder.Entity<Clube>().Property(p => p.Nome).IsRequired();
            builder.Entity<Clube>().Property(p => p.Decricao).IsRequired();
            builder.Entity<Clube>().Property(p => p.DataCadastro).IsRequired();

            builder.Entity<Dependente>().ToTable("Dependetes", "ClubeDB");
            builder.Entity<Dependente>().HasKey(p => p.SocioId);
            builder.Entity<Dependente>().Property(p => p.Nome).IsRequired();
            builder.Entity<Dependente>().Property(p => p.DataNacimento).IsRequired();
            builder.Entity<Dependente>().HasOne(p => p.Socio).WithMany(p => p.Dependentes);

            builder.Entity<Funcionario>().ToTable("Funcionarios", "ClubeDB");
            builder.Entity<Funcionario>().HasKey(p => p.FuncionarioId);
            builder.Entity<Funcionario>().Property(p => p.Nome).IsRequired();
            builder.Entity<Funcionario>().Property(p => p.DataNacimento).IsRequired();

            builder.Entity<HistorioFuncionario>().ToTable("HistoriosFuncionarios", "ClubeDB");
            builder.Entity<HistorioFuncionario>().HasKey(mf => new { mf.MensalidadeId, mf.FuncionarioId });

            builder.Entity<Mensalidade>().ToTable("Mensalidades", "ClubeDB");
            builder.Entity<Mensalidade>().HasKey(p => p.MensalidadeId);
            builder.Entity<Mensalidade>().Property(p => p.DataMensalidade).IsRequired();
            builder.Entity<Mensalidade>().Property(p => p.DataVencimento).IsRequired();
            builder.Entity<Mensalidade>().Property(p => p.Valor).IsRequired();
            builder.Entity<Mensalidade>().Property(p => p.Juros).IsRequired();
            builder.Entity<Mensalidade>().Property(p => p.Pago).IsRequired();
            builder.Entity<Mensalidade>().HasOne(p => p.Socio).WithMany(p => p.Mensalidades);

            builder.Entity<Socio>().ToTable("Socios", "ClubeDB");
            builder.Entity<Socio>().HasKey(p => p.SocioId);
            builder.Entity<Socio>().Property(p => p.Nome).IsRequired();
            builder.Entity<Socio>().Property(p => p.DataNacimento).IsRequired();

            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable(name: "User", schema: "ClubeDB");
            builder.Entity<IdentityRole>().ToTable(name: "Role", schema: "ClubeDB");

            builder.Entity<IdentityUserRole<string>>().ToTable(name: "UserRole", schema: "ClubeDB");
            builder.Entity<IdentityUserClaim<string>>().ToTable(name: "UserClaim", schema: "ClubeDB");
            builder.Entity<IdentityUserLogin<string>>().ToTable(name: "UserLogin", schema: "ClubeDB");

            builder.Entity<IdentityRoleClaim<string>>().ToTable(name: "RoleClaim", schema: "ClubeDB");
            builder.Entity<IdentityUserToken<string>>().ToTable(name: "UserToken", schema: "ClubeDB");

        }
    }
}
