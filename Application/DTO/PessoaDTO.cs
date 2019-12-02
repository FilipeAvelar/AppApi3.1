using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class PessoaDTO
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        [MaxLength(100)]
        public string NomeCompleto { get; set; }
        [MaxLength(100)]
        public string RazaoSocial { get; set; }
    }
}
