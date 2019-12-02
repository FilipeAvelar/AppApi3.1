using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class ClienteDTO
    {
        public ClienteDTO()
        {
            Contatos = new List<ContatoDTO>();
            PessoaDTO = new PessoaDTO();
        }

        public int ID { get; set; }
        public PessoaDTO PessoaDTO { get; set; }
        public string CEP { get; set; }
        [Required(ErrorMessage ="O campo e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage="O campo classificação é obrigatório")]
        public ClassificacaoDTO ClassificacaoDTO { get; set; }
        public List<ContatoDTO> Contatos { get; set; }
        public TipoPessoaDTO TipoPessoaDTO { get; set; }
    }
}
