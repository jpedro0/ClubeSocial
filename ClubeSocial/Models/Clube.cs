using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Clube
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Decricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public IList<Socio> Socios { get; set; }

        public bool AvaliarPedido(Candidato candidato)
        {
            return true;
        }
    }
}
