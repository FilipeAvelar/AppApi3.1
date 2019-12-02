using Domain.Enum;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Contatos = new List<Contato>();
        }

        public int ID { get; set; }
        public Pessoa Pessoa { get; set; }
        public string CEP { get; set; }
        public string Email { get; set; }
        public Classificacao Classificacao { get; set; }
        public ICollection<Contato> Contatos { get; set; }
    }
}
