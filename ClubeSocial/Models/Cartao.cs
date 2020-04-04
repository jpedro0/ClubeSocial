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
        public int ClubeId { get; set; }
        public Clube Clube { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Valido { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }
    }
}
