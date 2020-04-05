using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository
{
    public class ClubeRepository : RepositoryBase<Clube>, IClubeRepository
    {
        public ClubeRepository(ClubeDBContext context) : base(context)
        {
        }
    }
}
