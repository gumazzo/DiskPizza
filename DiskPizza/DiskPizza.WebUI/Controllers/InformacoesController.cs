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
            return View();
        }
    }
}