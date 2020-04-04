using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Funcionario: Pessoa
    {
        public int FuncionarioId { get; set; }
        public IList<HistorioFuncionario> HistorioFuncionarios { get; set; }
    }
}
