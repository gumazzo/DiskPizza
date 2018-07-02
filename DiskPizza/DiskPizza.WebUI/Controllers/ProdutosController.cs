using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.Models;
using DiskPizza.DataAccess;

namespace DiskPizza.WebUI.Controllers
{
    public class ProdutosController : Controller
    {
        public ActionResult Index()
        {
            var lstProdutos = new ProdutoDAO().BuscarTodos();
            return View(lstProdutos);
        }

        public ActionResult CadProd()
        {
            ViewBag.Tamanhos = new TamanhoDAO().BuscarTodos();
            return View();
        }

        public ActionResult Salvar(Produto obj)
        {
            new ProdutoDAO().Inserir(obj);

            return RedirectToAction("Index", "Produtos");
        }
    }
}