using System;

namespace ClubeSocial.Models
{
    public class Mensalidade
    {
        public int Id { get; set; }
        public DateTime DataMensalidade { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public bool Pago { get; set; }

        public void CalcularJutos()
        {

        }
    }
}