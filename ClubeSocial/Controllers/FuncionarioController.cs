using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClubeSocial.Models;
using ClubeSocial.Repository.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubeSocial.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class FuncionarioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISocioRepository _socioRepository;
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IMensalidadeRepository _mensalidadeRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        public FuncionarioController(
            ISocioRepository socioRepository,
            UserManager<IdentityUser> userManager,
            ICartaoRepository cartaoRepository,
            IMensalidadeRepository mensalidadeRepository,
            IFuncionarioRepository funcionarioRepository)
        {
            _cartaoRepository = cartaoRepository;
            _socioRepository = socioRepository;
            _userManager = userManager;
            _mensalidadeRepository = mensalidadeRepository;
            _funcionarioRepository = funcionarioRepository;
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

        public async Task<IActionResult> GerarCartao(int id)
        {
            var socio = _socioRepository.BuscaSocioPorId(id);
            var usuario = await _userManager.FindByEmailAsync(socio.Email);
            if (usuario != null)
            {
                await _userManager.RemoveClaimAsync(usuario, new Claim("socio", "andamento"));
                await _userManager.AddClaimAsync(usuario, new Claim("socio", "dependentes"));
                Cartao cartao = new Cartao().CreateCartao(socio);
                _cartaoRepository.Add(cartao);
                return Json(new { mensagem = "Concluindo" });
            }
            return Json(new { mensagem = "Erro" });
        }

        public async Task<IActionResult> GerarMensalidade(int id)
        {
            var usuarioFuncionario = await _userManager.GetUserAsync(User);
            var funcionario = _funcionarioRepository.BuscaFuncionarioPorEmail(usuarioFuncionario.Email);
            var socio = _socioRepository.BuscaSocioPorId(id);
            var usuario = await _userManager.FindByEmailAsync(socio.Email);
            if (usuario != null)
            {
                Mensalidade mensalidade = new Mensalidade().CreateMensalidade(funcionario, socio);
                _mensalidadeRepository.Add(mensalidade);
                return Json(new { mensagem = "Concluindo" });
            }
            return Json(new { mensagem = "Erro" });
        }

        public IActionResult BuscaMensalidade(int? numeroCartao = null)
        {
            Cartao cartao = null;
            if (numeroCartao.HasValue)
                cartao = _cartaoRepository.BuscaCartaoPorNumeroCartao(numeroCartao.Value);

            return View(cartao);
        }

        public async Task<IActionResult> QuitarMensalidade(int id)
        {
            var usuarioFuncionario = await _userManager.GetUserAsync(User);
            var funcionario = _funcionarioRepository.BuscaFuncionarioPorEmail(usuarioFuncionario.Email);
            var mensalidade = _mensalidadeRepository.BuscaMensalidadePorId(id);
            if (mensalidade != null)
            {
                mensalidade.Pago = true;
                mensalidade.CalcularJutos();
                mensalidade.HistorioFuncionarios.Add(
                new HistorioFuncionario 
                {
                    Descricao = "Mensalidade Paga",
                    Funcionario = funcionario,
                    Mensalidade = mensalidade
                });
                _mensalidadeRepository.Update(mensalidade);
                return Json(new { mensagem = "Concluindo" });
            }
            return Json(new { mensagem = "Erro" });
        }

    }
}