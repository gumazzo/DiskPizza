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
        public ActionResult Index(Pedido obj)
        {
            obj.Data = DateTime.Now;
            obj.Usuario = new Usuario() { Id = ((Usuario)User).Id };
            new PedidoDAO().Inserir(obj);

            ViewBag.Tamanhos = new TamanhoDAO().BuscarTodos();
            var lista = new Prod_x_TamanhoDAO().BuscarTodos();
            return View(lista);
        }
    }
}