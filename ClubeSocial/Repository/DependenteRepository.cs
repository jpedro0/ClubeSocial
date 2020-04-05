using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository
{
    public class DependenteRepository : RepositoryBase<Dependente>, IDependenteRepository
    {
        public DependenteRepository(ClubeDBContext context) : base(context)
        {
        }

        public IList<Dependente> BuscaDependentePorSocioEmail(string email)
        {
            return DbSet
                .Where(p => p.Socio.Email == email)
                .ToList();
        }
    }
}
