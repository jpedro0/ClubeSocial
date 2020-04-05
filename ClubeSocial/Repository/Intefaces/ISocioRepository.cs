using ClubeSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository.Intefaces
{
    public interface ISocioRepository : IRepositoryBase<Socio>
    {
        Socio BuscaSocioPorId(int id);
        Socio BuscaSocioPorEmail(string email);
        IList<Socio> BuscaSocioPorCartaoNull();
        IList<Socio> BuscaSocioPorMensalidadeNullMesAtual();
    }
}
