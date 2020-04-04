using System;
using System.Collections.Generic;

namespace ClubeSocial.Models
{
    public class Mensalidade
    {
        public int MensalidadeId { get; set; }
        public DateTime DataMensalidade { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public bool Pago { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }
        public IList<HistorioFuncionario> HistorioFuncionarios { get; set; }

        public void CalcularJutos()
        {

        }
    }
}