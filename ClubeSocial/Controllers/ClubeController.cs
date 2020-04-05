using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubeSocial.Repository.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubeSocial.Controllers
{
    [Authorize(Roles = "Clube")]
    public class ClubeController : Controller
    {
        private readonly ICandidatoRepository _candidatoRepository;
        public ClubeController(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }

        public IActionResult Index()
        {
            var listaCandidato = _candidatoRepository.BuscaTodosCandidatoEmAvaliacao();
            return View(listaCandidato);
        }
    }
}