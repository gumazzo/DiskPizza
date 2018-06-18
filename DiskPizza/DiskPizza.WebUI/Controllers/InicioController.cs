using DiskPizza.DataAccess;
using DiskPizza.Models;
using System.Web.Mvc;

namespace DiskPizza.WebUI.Controllers
{
    public class InicioController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Salvar(Usuario obj)
        {
            new UsuarioDAO().Inserir(obj);

            return RedirectToAction("Index", "Inicio");
        }
    }
}