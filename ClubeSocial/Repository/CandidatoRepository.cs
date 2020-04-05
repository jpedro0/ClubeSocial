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
    }
}
