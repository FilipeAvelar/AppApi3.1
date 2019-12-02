using Domain.Enum;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task ExcluirCliente(int ID);
        Task InserirCliente(Cliente cliente);
        Task<IEnumerable<Cliente>> ListarClientes(TipoPessoa? tipoPessoa = null);
        Task AlterarCliente(Cliente cliente);
        Task<Cliente> BuscarPorID(int ID);
    }
}
