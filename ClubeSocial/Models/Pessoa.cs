using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models
{
    public abstract class Pessoa
    {
        [Required(ErrorMessage = "O campo não pode estar vazio")]
        [EmailAddress(ErrorMessage = "O campo email não é um endereço de email válido.")]
        public string Email { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Pelido { get; set; }
        [Required]
        public DateTime DataNacimento { get; set; }
    }
}
