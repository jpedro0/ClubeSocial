using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubeSocial.Repository.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubeSocial.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class FuncionarioController : Controller
    {
        private readonly ISocioRepository _socioRepository;
        public FuncionarioController(ISocioRepository socioRepository)
        {
            _socioRepository = socioRepository;
        }
        public IActionResult Index() => View();

        public IActionResult ListaSocioNovos()
        {
            var listasocio = _socioRepository.BuscaSocioPorCartaoNull();
            return View(listasocio);
        }

        public IActionResult ListaMensalidadeAGerar()
        {
            var listamensalidade = _socioRepository.BuscaSocioPorMensalidadeNullMesAtual();
            return View(listamensalidade);
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