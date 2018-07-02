using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiskPizza.Models;
using DiskPizza.DataAccess;

namespace DiskPizza.WebUI.Controllers
{
    public class PagamentoController : Controller
    {
        public ActionResult Index(int id)
        {
            var cartao = new CartaoDeCredito();
            cartao.Pedido = new PedidoDAO().BuscarPorId(id);
            return View(cartao);
        }

        public ActionResult FazerPgto(CartaoDeCredito obj)
        {
            var pedido = new PedidoDAO().BuscarPorId(obj.Pedido.Id);

            //caso não encontre o pedido na base de dados irá retornar um erro
            if (pedido == null)
            {
                ViewBag.ErroMsg = "NÃO FOI POSSÍVEL REALIZAR A TRANSAÇÃO, TENTE NOVAMENTE MAIS TARDE!";
                return View("Index");
            }

            //validando se todos os dados do cartão de crédito foram preenchidos
            if (!Validar(obj))
            {
                pedido.Status = "PAGAMENTO RECUSADO";
                ViewBag.ErroMsg = "PAGAMENTO RECUSADO";
                return View("Index");
            }

            //se tudo estiver ok mudar o status do pedido para pagamento realizado
            pedido.Status = "PAGAMENTO REALIZADO";
            new PedidoDAO().Atualizar(pedido);

            return RedirectToAction("MeusPedidos", "Principal");
        }

        private bool Validar(CartaoDeCredito obj)
        {
            var aux = true;

            if (obj == null)
                aux = false;

            if (string.IsNullOrWhiteSpace(obj.NomeDoTitular))
                aux = false;

            if (string.IsNullOrWhiteSpace(obj.NumeroCartao))
                aux = false;

            if (string.IsNullOrWhiteSpace(obj.ValidadeCartao))
                aux = false;

            if (string.IsNullOrWhiteSpace(obj.CodVerCartao))
                aux = false;

            if (obj.CodVerCartao != "123")
                aux = false;

            return aux;
        }
    }
}