using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_NetCore3.1.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteApplication _ClienteApplication;

        public ClienteController(IClienteApplication clienteApplication)
        {
            _ClienteApplication = clienteApplication;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ClienteApplication.ListarClientes());
        }

        [HttpPost]
        public async Task<IActionResult> Listar(TipoPessoaDTO? tipoPessoa = null)
        {
            return View(await _ClienteApplication.ListarClientes(tipoPessoa));
        }

        public async Task<IActionResult> Cadastrar(int? ID = null)
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            if (ID.HasValue && ID.Value > 0)
                clienteDTO = await _ClienteApplication.BuscarPorID(ID.Value);

            return View(clienteDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarEditar(ClienteDTO clienteDTO)
        {
            if(!ModelState.IsValid)
            {
                return View(@"~/Views/Cliente/Cadastrar.cshtml", clienteDTO);
            }
            try
            {
                await _ClienteApplication.InserirAlterarCliente(clienteDTO);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View(@"~/Views/Cliente/Cadastrar.cshtml", clienteDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int ID)
        {
            await _ClienteApplication.ExcluirCliente(ID);
            var clientes = await _ClienteApplication.ListarClientes();
            return PartialView(@"~/Views/Cliente/Listar.cshtml", clientes);
        }
    }
}
