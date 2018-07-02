using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.Models;
using DiskPizza.DataAccess;

namespace DiskPizza.WebUI.Controllers
{
    public class PedidosController : Controller
    {
        public ActionResult Index()
        {
            var lstPedidos = new PedidoDAO().BuscarTodos();
            return View(lstPedidos);
        }

        public ActionResult VerDetalhes(int id)
        {
            var pedido = new PedidoDAO().BuscarPorId(id);
            pedido.Itens = new Item_PedidoDAO().BuscarPorPedido(id);
            return View(pedido);
        }
    }
}