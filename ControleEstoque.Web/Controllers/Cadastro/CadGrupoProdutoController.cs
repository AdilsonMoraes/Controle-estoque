using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class CadGrupoProdutoController : Controller
    {
        // Abre a view com a lista acima, caso queira usar a lista.
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { VarGlobal.QuantidadeMaxLinhaPorPagina, 10, 15, 20}, VarGlobal.QuantidadeMaxLinhaPorPagina);
            ViewBag.QuantidadeMaxLinhaPorPagina = VarGlobal.QuantidadeMaxLinhaPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = GrupoProdutoModel.RecuperarLista(ViewBag.PaginaAtual, VarGlobal.QuantidadeMaxLinhaPorPagina);
            var Quant = GrupoProdutoModel.RecuperarQuantidade();
            var difQuantPaginas = (Quant % ViewBag.QuantidadeMaxLinhaPorPagina) > 0 ? 1 : 0; //Se tiver resto joga 1
            ViewBag.QuantPaginas = difQuantPaginas + (lista.Count / ViewBag.QuantidadeMaxLinhaPorPagina); //Divide a qtde de registro somando o resto.

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult GrupoProdutoPagina(int pagina, int tamPag)
        {
            var lista = GrupoProdutoModel.RecuperarLista(pagina, tamPag);

            return Json(lista);
        }

        //Recupera o registro para confirmar ou excluir
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.RecuperarPerloId(id));
        }

        //Passa o carro no registro
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.ExcluirPeloId(id));
        }

        //Salva adicionando o item na lista
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarGrupoProduto(GrupoProdutoModel model)
        {
            string msg = GlobalMsg.RetornaMsg((int)GlobalMsg.OptionErro.OK);
            
            var resultado = msg;
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            //Validação
            if (!ModelState.IsValid)
            {
                msg = GlobalMsg.RetornaMsg((int)GlobalMsg.OptionErro.AVISO);
                resultado = msg;

                //lista de mensagem
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    //Busca o Registro
                    var id = model.Salvar();

                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        msg = GlobalMsg.RetornaMsg((int)GlobalMsg.OptionErro.ERRO);
                        resultado = msg;
                    }

                }
                catch (Exception ex)
                {
                    msg = GlobalMsg.RetornaMsg((int)GlobalMsg.OptionErro.ERRO);
                    resultado = msg;
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }


    }
}