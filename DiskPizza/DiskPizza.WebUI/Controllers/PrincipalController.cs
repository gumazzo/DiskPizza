﻿using DiskPizza.DataAccess;
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
            //Criar pedido usuário
            var pedido = new Pedido();
            pedido.Data = DateTime.Now;
            pedido.Usuario = new Usuario() { Id = ((Usuario)User).Id };


            //Salvar no banco de dados

            //Criar um item para o pedido

            //Salvar item que criou

            //Pegar o pedido objeto com o item dentro dele e passar para a partialView

            return PartialView("_Pedido");
        }
    }
}