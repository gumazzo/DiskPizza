using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.Models;
using DiskPizza.DataAccess;

namespace DiskPizza.WebUI.Views.ListProd
{
    public class ListProdController : Controller
    {
        // GET: ListProd
        public ActionResult Index()
        {
            var lstProdutos = new List<Produto>();

            lstProdutos = new ProdutoDAO().BuscarTodos();


            return View(lstProdutos);
        }

        public ActionResult Produto()
        {
            return View();
        }

    }
}