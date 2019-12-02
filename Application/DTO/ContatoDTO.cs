using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class ContatoDTO
    {
        public int ID { get; set; }
        public int IDCliente { get; set; }
        [MaxLength(15)]
        [Required]
        public string Telefone { get; set; }
    }
}
