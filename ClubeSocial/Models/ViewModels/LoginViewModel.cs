using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo não pode estar vazio")]
        [EmailAddress(ErrorMessage = "O campo email não é um endereço de email válido.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo de senha é necessário.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Display(Name = "Lembrar-me ")]
        public bool Salvar { get; set; }

    }
}
