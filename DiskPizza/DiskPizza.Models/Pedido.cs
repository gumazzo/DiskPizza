using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskPizza.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int QtdSabores { get; set; }
        public string Status { get; set; }
        public Usuario Usuario { get; set; }
        public List<Item_Pedido> Itens { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public decimal ValorTotal
        {
            get { return this.Itens.Sum(item => item.Preco_item); }
        }

        public Pedido()
        {
            this.Itens = new List<Item_Pedido>();
        }
    }
}
