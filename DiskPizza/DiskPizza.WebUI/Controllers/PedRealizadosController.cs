using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.Models;
using DiskPizza.DataAccess;

namespace DiskPizza.WebUI.Views.PedRealizados
{
    public class PedRealizadosController : Controller
    {
        // GET: PedRealizados
        public ActionResult Index()
        {
            var lstPedidos = new List<Pedido>();

            lstPedidos = new PedidoDAO().BuscarTodos();

            return View(lstPedidos);
        }

        public ActionResult Pedido()
        {
            return View();
        }

    }
}