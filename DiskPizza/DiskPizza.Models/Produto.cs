using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskPizza.Models
{
    public class Produto
    {
        public int       Id      { get; set; }
        public string    Nome    { get; set; }
        public string    Tipo    { get; set; }
        public decimal     Preco   { get; set; }
    }
}
