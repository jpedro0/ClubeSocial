using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository
{
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(ClubeDBContext context) : base(context)
        {
        }
        public Funcionario BuscaFuncionarioPorEmail(string email)
        {
            return DbSet.FirstOrDefault(p => p.Email == email);
        }
    }
}
