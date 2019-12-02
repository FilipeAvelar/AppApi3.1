using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.DomainService.Interface;
using Domain.Enum;
using Domain.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Classes
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IClienteRepository _ClienteRepository;
        private readonly IMapper _AutoMapper;
        private readonly IClienteService _ClienteService;

        public ClienteApplication(IClienteRepository clienteRepository,
                                  IMapper mapper,
                                  IClienteService clienteService)
        {
            _ClienteRepository = clienteRepository;
            _AutoMapper = mapper;
            _ClienteService = clienteService;
        }

        public async Task ExcluirCliente(int ID)
        {
            await _ClienteRepository.ExcluirCliente(ID);
        }

        public async Task<ClienteDTO> BuscarPorID(int ID)
        {
            var cliente = await _ClienteRepository.BuscarPorID(ID);
            return RetornarPessoaDTO(cliente);
        }

        public async Task InserirAlterarCliente(ClienteDTO clienteDTO)
        {
            var cliente = _AutoMapper.Map<ClienteDTO, Cliente>(clienteDTO);
            cliente.Classificacao = (Classificacao)clienteDTO.ClassificacaoDTO;
            if(clienteDTO.TipoPessoaDTO == TipoPessoaDTO.Fisica)
            {
                cliente.Pessoa = new PessoaFisica()
                {
                    CPF = clienteDTO.PessoaDTO.CPF,
                    NomeCompleto = clienteDTO.PessoaDTO.NomeCompleto,
                    ID = clienteDTO.PessoaDTO.ID
                };
            }
            else
            {
                cliente.Pessoa = new PessoaJuridica()
                {
                    CNPJ = clienteDTO.PessoaDTO.CNPJ,
                    RazaoSocial = clienteDTO.PessoaDTO.RazaoSocial,
                    ID = clienteDTO.PessoaDTO.ID
                };
            }
            _ClienteService.ValidarRegrasCliente(cliente);

            if (cliente.ID == 0)
                await _ClienteRepository.InserirCliente(cliente);
            else
               await _ClienteRepository.AlterarCliente(cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> ListarClientes(TipoPessoaDTO? tipoPessoaDTO = null)
        {
            var clientes = await _ClienteRepository.ListarClientes(tipoPessoaDTO.HasValue ?
                                                                    (TipoPessoa)tipoPessoaDTO.Value :
                                                                    new Nullable<TipoPessoa>());
            var clientesDTO = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                clientesDTO.Add(RetornarPessoaDTO(cliente));
            }

            return clientesDTO; 
        }

        private ClienteDTO RetornarPessoaDTO(Cliente cliente)
        {
            var clienteDTO = _AutoMapper.Map<Cliente, ClienteDTO>(cliente);
            clienteDTO.ClassificacaoDTO = (ClassificacaoDTO)cliente.Classificacao;
            if (cliente.Pessoa is PessoaFisica pessoaFisica)
            {
                clienteDTO.PessoaDTO = new PessoaDTO
                {
                    CPF = Convert.ToUInt64(pessoaFisica.CPF).ToString(@"000\.000\.000\-00"),
                    NomeCompleto = pessoaFisica.NomeCompleto,
                    ID = pessoaFisica.ID
                };
                clienteDTO.TipoPessoaDTO = TipoPessoaDTO.Fisica;
            }
            else
            {
                PessoaJuridica pessoaJuridica = cliente.Pessoa as PessoaJuridica;
                clienteDTO.PessoaDTO = new PessoaDTO
                {
                    CNPJ = Convert.ToUInt64(pessoaJuridica.CNPJ).ToString(@"00\.000\.000\/0000-00"),
                    RazaoSocial = pessoaJuridica.RazaoSocial,
                    ID = pessoaJuridica.ID
                };
                clienteDTO.TipoPessoaDTO = TipoPessoaDTO.Juridica;
            }

            foreach (var contato in clienteDTO.Contatos.Where(x => !string.IsNullOrEmpty(x.Telefone)))
            {
               contato.Telefone = Convert.ToUInt64(contato.Telefone).ToString(@"(00)0000-0000");
            }

            return clienteDTO;
        }
    }
}
