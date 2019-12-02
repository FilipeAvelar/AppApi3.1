using Domain.DomainService.Interface;
using Domain.DomainService.Utils;
using Domain.Models;
using System;

namespace Domain.DomainService.Classe
{
    public class ClienteService : IClienteService
    {
        const int TAMANHO_MAXIMO_CAMPO_NOME = 100;
        public void ValidarRegrasCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new SystemException("Cliente não informado");

            if(cliente.Pessoa is PessoaFisica pessoaFisica)
            {
                if(string.IsNullOrEmpty(pessoaFisica.NomeCompleto) ||
                   string.IsNullOrWhiteSpace(pessoaFisica.NomeCompleto))
                    throw new SystemException("Nome Completo não informado");

                if (!string.IsNullOrEmpty(pessoaFisica.NomeCompleto) && 
                  pessoaFisica.NomeCompleto.Length > TAMANHO_MAXIMO_CAMPO_NOME)
                    throw new SystemException("Nome Completo deve possuir 100 caracteres.");

                if (!string.IsNullOrEmpty(pessoaFisica.CPF))
                    pessoaFisica.CPF = pessoaFisica.CPF.SomenteNumeros();
            }

            if (cliente.Pessoa is PessoaJuridica pessoaJuridica)
            {
                if (string.IsNullOrEmpty(pessoaJuridica.RazaoSocial) ||
                    string.IsNullOrWhiteSpace(pessoaJuridica.RazaoSocial))
                        throw new SystemException("Razão Social não informado");

                if (pessoaJuridica.RazaoSocial.Length > TAMANHO_MAXIMO_CAMPO_NOME)
                    throw new SystemException("Razão Social deve possuir 100 caracteres.");

                if (!string.IsNullOrEmpty(pessoaJuridica.CNPJ))
                    pessoaJuridica.CNPJ = pessoaJuridica.CNPJ.SomenteNumeros();
            }

            if(!string.IsNullOrEmpty(cliente.CEP))
                cliente.CEP = cliente.CEP.SomenteNumeros();

            if (string.IsNullOrEmpty(cliente.Email))
                throw new SystemException("E-mail não informado");

            foreach (var contato in cliente.Contatos)
            {
                contato.Cliente = cliente;
                contato.ClienteID = cliente.ID;
                contato.Telefone = contato.Telefone.SomenteNumeros();
            }
        }
    }
}
