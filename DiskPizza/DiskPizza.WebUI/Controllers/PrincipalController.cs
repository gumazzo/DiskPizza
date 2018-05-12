using DiskPizza.DataAccess;
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
    }
}