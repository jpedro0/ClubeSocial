using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class HistorioFuncionario
    {
        public int HistorioFuncionarioId { get; set; }
        public string Descricao { get; set; }

        public int MensalidadeId { get; set; }
        public Mensalidade Mensalidade { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
