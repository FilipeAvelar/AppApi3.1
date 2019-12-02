namespace Domain.Models
{
    public class Contato
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public string Telefone { get; set; }
        public Cliente Cliente { get; set; }
    }
}
