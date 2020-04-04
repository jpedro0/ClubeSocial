using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Cartao
    {
        public int NumeroDoCartao { get; set; }
        public string Nome { get; set; }
        public string Clube { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Valido { get; set; }
    }
}
