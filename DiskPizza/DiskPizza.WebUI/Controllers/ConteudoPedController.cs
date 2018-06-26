using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.Models;
using DiskPizza.DataAccess;

namespace DiskPizza.WebUI.Views.ConteudoPed
{
    public class ConteudoPedController : Controller
    {
        // GET: ConteudoPed
        public ActionResult Index()
        {
            var lstProdXtamanho = new List<Produto_x_Tamanho>();

            lstProdXtamanho = new Prod_x_TamanhoDAO().BuscarTodos();


            return View(lstProdXtamanho);
        }

        public ActionResult Prod_x_Tamanho()
        {
            return View();
        }

    }
}