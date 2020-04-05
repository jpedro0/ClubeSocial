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
    public class CartaoRepository : RepositoryBase<Cartao>, ICartaoRepository
    {
        public CartaoRepository(ClubeDBContext context) : base(context)
        {
        }

        public Cartao BuscaCartaoPorNumeroCartao(int numeroCartao)
        {
            return DbSet
                .Include(p => p.Socio)
                .ThenInclude(p => p.Mensalidades)
                .FirstOrDefault(p => p.NumeroDoCartao == numeroCartao);
        }
    }
}
