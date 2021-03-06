﻿using ClubeSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Repository.Intefaces
{
    public interface IFuncionarioRepository: IRepositoryBase<Funcionario>
    {
        Funcionario BuscaFuncionarioPorEmail(string email);
    }
}
