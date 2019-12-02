namespace Domain.Models
{
    public class PessoaJuridica : Pessoa
    {
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
    }
}
