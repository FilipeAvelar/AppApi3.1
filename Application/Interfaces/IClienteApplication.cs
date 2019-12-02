using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClienteApplication
    {
        Task<IEnumerable<ClienteDTO>> ListarClientes(TipoPessoaDTO? tipoPessoaDTO = null);
        Task InserirAlterarCliente(ClienteDTO clienteDTO);
        Task ExcluirCliente(int ID);
        Task<ClienteDTO> BuscarPorID(int ID);
    }
}
