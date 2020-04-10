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
        private readonly IDependenteRepository _dependenteRepository;
        public SocioController(
            UserManager<IdentityUser> userManager,
            ICandidatoRepository candidatoRepository,
            ISocioRepository socioRepository,
            IDependenteRepository dependenteRepository)
        {
            _userManager = userManager;
            _candidatoRepository = candidatoRepository;
            _socioRepository = socioRepository;
            _dependenteRepository = dependenteRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Socio")]
        public async Task<IActionResult> Index()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var claims = await _userManager.GetClaimsAsync(usuario);
            if (claims.Any(p => p.Type == "socio" && p.Value == "andamento"))
                return RedirectToAction(nameof(SocioAndamento));
            else if (claims.Any(p => p.Type == "socio" && p.Value == "dependentes"))
                return RedirectToAction(nameof(ListarDependente));
            var socio = _socioRepository.BuscaSocioPorEmail(usuario.Email);
            return View(socio);
        }

        [HttpGet]
        [Authorize(Policy = "Socio Andamento")]
        public IActionResult SocioAndamento() => View();

        [HttpGet]
        [Authorize(Roles = "Candidato")]
        public IActionResult EsperaCandidato() => View();

        [HttpGet]
        [Authorize(Policy = "Associar Dependente")]
        public async Task<IActionResult> ListarDependente()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var listaDenpendente = _dependenteRepository.BuscaDependentePorSocioEmail(usuario.Email);
            return View(listaDenpendente);
        }

        [HttpGet]
        [Authorize(Policy = "Associar Dependente")]
        public async Task<IActionResult> ConcluirDependente() 
        {
            var usuario = await _userManager.GetUserAsync(User);
            var socio = _socioRepository.BuscaSocioPorEmail(usuario.Email);
            await _userManager.RemoveClaimAsync(usuario, new Claim("socio", "dependentes"));
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        [Authorize(Policy = "Associar Dependente")]
        public IActionResult ExcluirDependente(int id) 
        {
            if (ModelState.IsValid)
            {
                _dependenteRepository.Remove(id);
                return RedirectToAction(nameof(ListarDependente));
            }
            return RedirectToAction(nameof(ListarDependente));
        }
        
        [HttpGet]
        [Authorize(Policy = "Associar Dependente")]
        public IActionResult AssociarDependente() 
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Associar Dependente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssociarDependente(Dependente dependente)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.GetUserAsync(User);
                var socio = _socioRepository.BuscaSocioPorEmail(usuario.Email);
                dependente.Socio = socio;
                _dependenteRepository.Add(dependente);
                return RedirectToAction(nameof(ListarDependente));
            }
            return View(dependente);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Candidato() => View();

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
                        _candidatoRepository.Add(model.Candidato);
                        await _userManager.AddToRoleAsync(user, "Candidato");
                        return RedirectToAction(nameof(EsperaCandidato));
                    }
                    ModelState.AddModelError(String.Empty, "Nome Invalido!");
                }
                ModelState.AddModelError(String.Empty, "Email ou Senha Invalido!");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Clube")]
        public async Task<IActionResult> ExcluirCondidato(int id)
        {
            var candidato = _candidatoRepository.BuscaCandidatoPorId(id);
            var usuario = await _userManager.FindByEmailAsync(candidato.Email);
            if (usuario != null)
            {
                await _userManager.RemoveFromRolesAsync(usuario, new string[] { "Candidato" });
                await _userManager.AddToRoleAsync(usuario, "Reprovado");
                _candidatoRepository.Remove(candidato);
                return Json(new { mensagem = "Concluindo" });
            }
            return Json(new { mensagem = "Erro" });
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
                await _userManager.AddClaimAsync(usuario, new Claim("socio", "andamento"));
                candidato.Situacao = Situacao.Aprovado;
                _candidatoRepository.Update(candidato);
                Socio socio = new Socio().CreateSocio(candidato);
                _socioRepository.Add(socio);
                return Json(new { mensagem = "Concluindo" });
            }
            return Json(new { mensagem = "Erro" });
        }
    }
}