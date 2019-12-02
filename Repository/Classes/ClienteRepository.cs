using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Classes
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly VettaDBContext _VettaDBContext;

        public ClienteRepository(VettaDBContext vettaDBContext)
        {
            _VettaDBContext = vettaDBContext;
        }

        public async Task ExcluirCliente(int ID)
        {
            var cliente = await _VettaDBContext.Clientes.FirstOrDefaultAsync(x => x.ID == ID);
            if (cliente == null)
                throw new SystemException("Cliente não encontrado");

            _VettaDBContext.Clientes.Remove(cliente);
            await _VettaDBContext.SaveChangesAsync();
        }

        public async Task InserirCliente(Cliente cliente)
        {
            _VettaDBContext.Clientes.Add(cliente);
            await _VettaDBContext.SaveChangesAsync();
        }

        public async Task AlterarCliente(Cliente cliente)
        {
            var clienteBanco = await BuscarPorID(cliente.ID);
            _VettaDBContext.Entry(clienteBanco).CurrentValues.SetValues(cliente);
            _VettaDBContext.Entry(clienteBanco.Pessoa).CurrentValues.SetValues(cliente.Pessoa);

            foreach (var contato in clienteBanco.Contatos.ToList())
            {
                if (!cliente.Contatos.Any(c => c.ID == contato.ID))
                    _VettaDBContext.Contatos.Remove(contato);
            }

            foreach (var contato in cliente.Contatos)
            {
                var contatoBanco = clienteBanco.Contatos
                                               .Where(c => c.ID == contato.ID && c.ID != default(int))
                                               .SingleOrDefault();

                if (contatoBanco != null)
                    _VettaDBContext.Entry(contatoBanco).CurrentValues.SetValues(contato);
                else
                    clienteBanco.Contatos.Add(contato);
            }

            await _VettaDBContext.SaveChangesAsync();
        }

        public async Task<Cliente> BuscarPorID(int ID)
        {

            return await _VettaDBContext.Clientes.Include(t => t.Contatos)
                                                 .Include(t => t.Pessoa)
                                                 .FirstOrDefaultAsync(t => t.ID == ID);
        }
        public async Task<IEnumerable<Cliente>> ListarClientes(TipoPessoa? tipoPessoa = null)
        {
            try
            {
                IQueryable<Cliente> queryCliente = _VettaDBContext.Clientes.Include(t => t.Contatos)
                                                                            .Include(t => t.Pessoa)
                                                                            .AsNoTracking();
                var clientes = await queryCliente.ToListAsync();
                if (tipoPessoa.HasValue)
                {
                    if (tipoPessoa == TipoPessoa.Fisica)
                        clientes = clientes.Where(t => t.Pessoa is PessoaFisica).ToList();
                    else
                        clientes = clientes.Where(t => t.Pessoa is PessoaJuridica).ToList();
                }

                return clientes;
            }
            catch(Exception ex)
            {
                return null;
            }          
        }
    }
}
