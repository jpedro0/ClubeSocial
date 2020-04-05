using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly ISocioRepository _socioRepository;
        public SocioController(
            UserManager<IdentityUser> userManager,
            ICandidatoRepository candidatoRepository,
            ISocioRepository socioRepository)
        {
            _userManager = userManager;
            _candidatoRepository = candidatoRepository;
            _socioRepository = socioRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Socio")]
        public IActionResult Index() => View();

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
                    _candidatoRepository.Add(model.Candidato);
                    IdentityResult result = await _userManager.CreateAsync(user, model.Senha);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Candidato");
                        return RedirectToAction(nameof(EsperaCandidato));
                    }
                }
                ModelState.AddModelError("", "Email ou Senha Invalido!");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Clube")]
        public async Task<IActionResult> AtualizarCondidatoSocio(int id)
        {
            var candidato = _candidatoRepository.BuscaCandidatoPorId(id);
            var usuario = await _userManager.FindByEmailAsync(candidato.Email);
            if (usuario != null)
            {
                await _userManager.RemoveFromRolesAsync(usuario, new string[] { "Candidato" });
                await _userManager.AddToRoleAsync(usuario, "Socio");
                candidato.Situacao = Situacao.Aprovado;
                _candidatoRepository.Update(candidato);
                var socio = new Socio
                {
                    Nome = candidato.Nome,
                    DataNacimento = candidato.DataNacimento,
                    Email = candidato.Email,
                    Pelido = candidato.Pelido
                };
                _socioRepository.Add(socio);
                return Json(new { mensagem = "Concluindo" });
            }
            return Json(new { mensagem = "Erro" });
        }
    }
}