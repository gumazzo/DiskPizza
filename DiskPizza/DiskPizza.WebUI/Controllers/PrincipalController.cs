using DiskPizza.DataAccess;
using DiskPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DiskPizza.WebUI.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Tipos = new ProdutoDAO().BuscarTodos().Select(o => o.Tipo).Distinct().ToList();
            ViewBag.Tamanhos = new TamanhoDAO().BuscarTodos();
            ViewBag.Pizzas = new List<Produto_x_Tamanho>();
            return View();
        }

        public ActionResult BuscarCardapio(int tamanho)
        {
            ViewBag.Tipos = new ProdutoDAO().BuscarTodos().Select(o => o.Tipo).Distinct().ToList();
            ViewBag.Tamanhos = new TamanhoDAO().BuscarTodos();
            ViewBag.Pizzas = new Prod_x_TamanhoDAO().BuscarPorTamanho(tamanho);
            return PartialView("_Cardapio");
        }

        public ActionResult SalvarPedido(Pedido obj)
        {
            obj.Data = DateTime.Now;
            obj.Usuario = new Usuario() { Id = ((Usuario)User).Id };
            new PedidoDAO().Inserir(obj);
            return View();
        }

        public ActionResult Finalizar(Pedido obj)
        {
            obj.Data = DateTime.Now;
            obj.Usuario = new Usuario() { Id = ((Usuario)User).Id };
            return View();
        }

        public ActionResult AdicionarItem(int produtoXtamanho, int quantidadeDeSabores)
        {
            //se tiver algum pedido pendente no banco de dados, usar o mesmo pedido par adicionar novos itens
            var pedido = new PedidoDAO().BuscarPorUsuario(((Usuario)User).Id);

            //buscar todos os dados do usuário logado no banco de dados
            var usuario = new UsuarioDAO().BuscarPorId(((Usuario)User).Id);

            //criando pedido e salvando este pedido no banco de dados
            if (pedido == null)
            {
                pedido = new Pedido();
                pedido.Data = DateTime.Now;
                pedido.Usuario = new Usuario() { Id = ((Usuario)User).Id };
                pedido.QtdSabores = quantidadeDeSabores;
                pedido.Status = "PENDENTE";

                //preenchendo os dados de entrega do pedido baseado no endereço do usuário
                pedido.Cep = usuario.Cep;
                pedido.Rua = usuario.Rua;
                pedido.Numero = usuario.Numero;

                //inserindo o pedido no banco de dados
                new PedidoDAO().Inserir(pedido);
            }

            //buscando no banco de dados produto x tamanho que foi selecionado na tela pelo usuário
            var pxt = new Prod_x_TamanhoDAO().BuscarPorId(produtoXtamanho);

            //adicionando item selecionado ao pedido
            var item = new Item_Pedido()
            {
                Pedido = pedido,
                Produto_x_Tamanho = pxt,
                Preco_item = pxt.Preco_Total / Convert.ToDecimal(quantidadeDeSabores)
            };

            //salvando item que foi criado
            new Item_PedidoDAO().Inserir(item);

            //recarregando todos os itens do pedido
            pedido.Itens = new Item_PedidoDAO().BuscarPorPedido(pedido.Id);

            //retornando a partial view com o pedido e seus itens
            return PartialView("_Pedido", pedido);
        }

        public ActionResult MeusPedidos()
        {
            var lst = new PedidoDAO().BuscarMeusPedidos(((Usuario)User).Id);
            lst.ForEach(p =>
            {
                p.Itens = new Item_PedidoDAO().BuscarPorPedido(p.Id);
            });
            return View(lst);
        }
    }
}