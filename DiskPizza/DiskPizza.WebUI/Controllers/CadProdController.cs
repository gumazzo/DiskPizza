using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.DataAccess;
using DiskPizza.Models;

namespace DiskPizza.WebUI.Views.CadProd
{
    public class CadProdController : Controller
    {
        // GET: CadProd
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Salvar(Produto obj)
        {
            new ProdutoDAO().Inserir(obj);

            return RedirectToAction("Index", "CadProd");
        }
    }
}