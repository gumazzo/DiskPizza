using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiskPizza.Models
{
    public class CartaoDeCredito
    {
        public Pedido Pedido { get; set; }
        public string NomeDoTitular { get; set; }
        public string NumeroCartao { get; set; }
        public string ValidadeCartao { get; set; }
        public string CodVerCartao { get; set; }
    }
}
