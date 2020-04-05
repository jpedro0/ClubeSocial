using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubeSocial.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class FuncionarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaSocioNovos()
        {
            return View();
        }

        public IActionResult ListaMensalidadeAGerar()
        {
            return View();
        }

        public IActionResult GerarCartao()
        {
            return View();
        }

        public IActionResult GerarMensalidade()
        {
            return View();
        }

        public IActionResult QuitarMensalidade()
        {
            return View();
        }
    }
}