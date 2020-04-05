using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository
{
    public class CandidatoRepository : RepositoryBase<Candidato>, ICandidatoRepository
    {
        public CandidatoRepository(ClubeDBContext context) : base(context)
        {
        }

        public Candidato BuscaCandidatoPorId(int id)
        {
            return DbSet.First(p => p.CandidatoId == id);
        }

        public IList<Candidato> BuscaTodosCandidatoEmAvaliacao()
        {
            return DbSet
                .Where(p => p.Situacao.Equals(Situacao.Avaliacao))
                .ToList();
        }
    }
}
