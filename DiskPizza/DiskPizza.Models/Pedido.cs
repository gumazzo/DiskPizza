using System;
using System.Collections.Generic;

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
        public List<Item_Pedido> Itens { get; set; }

        public Pedido()
        {
            this.Itens = new List<Item_Pedido>();
        }
    }
}
