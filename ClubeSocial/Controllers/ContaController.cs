using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubeSocial.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubeSocial.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ContaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {

            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(model.Login);

                if (usuario == null)
                    return View();

                var result =
                    await _signInManager.PasswordSignInAsync(
                         usuario.UserName,
                         model.Senha,
                         model.Salvar,
                         lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return await ValidaConta(usuario); 
                }

                if (result.IsLockedOut)
                {
                    var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, model.Senha);

                    if (senhaCorreta)
                        ModelState.AddModelError("", "Conta esta bloqueada!");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult TrocaSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrocaSenha(TrocaSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Login.Login);
                var validaSenha = await _userManager.CheckPasswordAsync(user, model.Login.Senha);

                if (user != null && validaSenha)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    if (ModelState.IsValid)
                    {
                        var resultadoAlteracao =
                            await _userManager.ResetPasswordAsync(
                                             user,
                                             token,
                                             model.NovaSenha);

                        if (resultadoAlteracao.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Login ou Senha Invalido!");
            }
            return View();
        }


        private async Task<IActionResult> ValidaConta(IdentityUser usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            return (roles.First()) switch
            {
                "Candidato" => RedirectToAction("EsperaCandidato", "Socio"),
                "Socio" => RedirectToAction("Index", "Socio"),
                "Clube" => RedirectToAction("Index", "Clube"),
                "Funcionario" => RedirectToAction("Index", "Funcionario"),
                _ => RedirectToAction(nameof(Login)),
            };
        }
    }
}