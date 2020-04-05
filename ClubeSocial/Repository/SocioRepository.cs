using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
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
    }
}
