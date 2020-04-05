using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeSocial.Models.ViewModels
{
    public class TrocaSenhaViewModel
    {
        public LoginViewModel Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confimar Senha", Prompt = "*******")]
        [Compare("NovaSenha", ErrorMessage = "Senha nao esta igual")]
        public string NovaSenhaConfimar { get; set; }
    }
}
