using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public class Candidato: Pessoa
    {
        public Situacao Situacao { get; set; }
        [Required]
        public int ClubeId { get; set; }
        public Clube Clube { get; set; }
    }
}
