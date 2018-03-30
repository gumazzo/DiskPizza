using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskPizza.Models
{
    public class Item_Pedido
    {
        public int               Id                  { get; set; }
        public Pedido            Pedido              { get; set; }
        public Produto_x_Tamanho Produto_x_Tamanho   { get; set; }
        public float             Preco_item          { get; set; }
    }
}
