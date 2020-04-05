using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubeSocial.Models;
using ClubeSocial.Models.ViewModels;
using ClubeSocial.Repository.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubeSocial.Controllers
{
    [Authorize]
    public class SocioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICandidatoRepository _candidatoRepository;
        public SocioController(
            UserManager<IdentityUser> userManager,
            ICandidatoRepository candidatoRepository)
        {
            _userManager = userManager;
            _candidatoRepository = candidatoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Candidato")]
        public IActionResult EsperaCandidato() => View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Candidato()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Candidato(CandidatoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Candidato.Nome,
                    Email = model.Candidato.Email
                };

                var usuario = await _userManager.FindByEmailAsync(model.Candidato.Email);
                if (usuario == null)
                {
                    IdentityResult result = await _userManager.CreateAsync(user, model.Senha);
                    if (result.Succeeded)
                    {
                        await _userManager
                            .AddToRoleAsync(user, "Candidato");
                        _candidatoRepository.Add(model.Candidato);
                        return RedirectToAction(nameof(EsperaCandidato));
                    }
                }
                ModelState.AddModelError("", "Email ou Senha Invalido!");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Socio")]
        public IActionResult Socio()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Socio")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Socio(Socio modal)
        {
            return View();
        }
    }
}