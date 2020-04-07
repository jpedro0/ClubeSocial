using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository
{
    public class SocioRepository : RepositoryBase<Socio>, ISocioRepository
    {
        public SocioRepository(ClubeDBContext context) : base(context)
        {
        }

        public Socio BuscaSocioPorId(int id)
        {
            return DbSet.First(p => p.SocioId == id);
        }

        public Socio BuscaSocioPorEmail(string email)
        {
            return DbSet.FirstOrDefault(p => p.Email == email);
        }

        public IList<Socio> BuscaSocioPorCartaoNull() 
        {
            return DbSet
                .Include(p => p.Cartao)
                .Where(p => p.Cartao == null)
                .ToList();
        }

        public IList<Socio> BuscaSocioPorMensalidadeNullMesAtual()
        {
            return DbSet
                .Include(p => p.Mensalidades)
                .Where(p => !p.Mensalidades.Any(p => p.DataMensalidade.Date == DateTime.Now.Date) &&
                    p.Dependentes.Any() && p.Cartao != null)
                .ToList();
        }
    }
}
