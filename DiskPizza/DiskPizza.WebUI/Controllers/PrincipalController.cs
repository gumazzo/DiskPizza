using DiskPizza.DataAccess;
using DiskPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiskPizza.WebUI.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Tipos = new ProdutoDAO().BuscarTodos().Select(o => o.Tipo).Distinct().ToList();
            ViewBag.Pizzas = new ProdutoDAO().BuscarTodos();
            return View();
        }

        public ActionResult AdicionarItem(Produto_x_Tamanho obj)
        {
            return View();
        }
    }
}