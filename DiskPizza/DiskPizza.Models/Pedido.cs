using System;

namespace DiskPizza.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Tamanho { get; set; }
        public int QtdSabores { get; set; }
        public string Status { get; set; }
        public Usuario Usuario { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string NumeroL { get; set; }
    }
}
