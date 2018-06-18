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
    public class InformacoesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Tamanhos = new TamanhoDAO().BuscarTodos();
            var lst = new Prod_x_TamanhoDAO().BuscarTodos();
            return View(lst);
        }
    }
}