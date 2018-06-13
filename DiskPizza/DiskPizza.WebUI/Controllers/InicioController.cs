using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.DataAccess;
using DiskPizza.Models;
using System.Web.Script.Serialization;

namespace DiskPizza.WebUI.Controllers
{
    [Authorize]
    public class InicioController : Controller
    {
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