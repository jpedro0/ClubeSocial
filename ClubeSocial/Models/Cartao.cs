using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Cartao
    {
        public int CartaoId { get; set; }
        public int NumeroDoCartao { get; set; }
        public string Nome { get; set; }
        public int ClubeId { get; set; }
        public Clube Clube { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Valido { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }

        public Cartao CreateCartao(Socio socio)
        {
            return new Cartao
            {
                NumeroDoCartao = new Random().Next(10000, 99999),
                Nome = socio.Nome,
                ClubeId = 1,
                DataVencimento = DateTime.Now.AddDays(300),
                Valido = true,
                SocioId = socio.SocioId
            };
        }
    }
}
