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

        [Authorize]
        public ActionResult SalvarCep(Usuario obj)
        {
            if (HttpContext.User.GetType() != typeof(Usuario))
            {
                return RedirectToAction("Index", "Login");
            }

            var usuarioLogado = new UsuarioDAO().BuscarPorId(((Usuario)HttpContext.User).Id);

            if (usuarioLogado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var u = new Usuario()
            {
                Id = ((Usuario)HttpContext.User).Id,
                Cep = obj.Cep,
                Rua = obj.Rua,
                Numero = obj.Numero
            };

            new UsuarioDAO().Atualizar(u);

            return RedirectToAction("Index", "Informacoes");
        }
    }
}