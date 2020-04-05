using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Socio: Pessoa
    {
        public int SocioId { get; set; }
        public Cartao Cartao { get; set; }
        public IList<Dependente> Dependentes { get; set; }
        public IList<Mensalidade> Mensalidades { get; set; }
    
        public void BuscaSocioPorNumeroDoCartao()
        {

        }
    }
}
