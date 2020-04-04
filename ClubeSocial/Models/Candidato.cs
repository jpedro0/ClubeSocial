using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Candidato: Pessoa
    {
        public Situacao Situacao { get; set; }
        public int ClubeId { get; set; }
        public Clube Clube { get; set; }
    }
}
