using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskPizza.Models
{
    public class Produto_x_Tamanho
    {
        public int       Id          { get; set; }
        public decimal     Preco_Total { get; set; }
        public Produto   Produto     { get; set; }
        public Tamanho   Tamanho     { get; set; }
    }
}
