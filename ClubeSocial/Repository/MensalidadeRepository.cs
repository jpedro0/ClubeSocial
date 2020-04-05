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
    public class MensalidadeRepository : RepositoryBase<Mensalidade>, IMensalidadeRepository
    {
        public MensalidadeRepository(ClubeDBContext context) : base(context)
        {
        }
        public Mensalidade BuscaMensalidadePorId(int id)
        {
            return DbSet.Include(p => p.HistorioFuncionarios).First(p => p.MensalidadeId == id);
        }
    }
}
