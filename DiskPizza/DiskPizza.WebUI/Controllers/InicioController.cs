using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.DataAccess;
using DiskPizza.Models;


namespace DiskPizza.WebUI.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salvar(Usuario obj)
        {
            new InicioDAO().Inserir(obj);

            return RedirectToAction("Index", "Inicio");
        }
    }
}