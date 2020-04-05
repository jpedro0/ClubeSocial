using ClubeSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository.Intefaces
{
    public interface ICartaoRepository: IRepositoryBase<Cartao>
    {
        Cartao BuscaCartaoPorNumeroCartao(int numeroCartao);
    }
}
