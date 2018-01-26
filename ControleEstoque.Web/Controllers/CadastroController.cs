using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class CadastroController : Controller
    {

        private const string _senhaPadrao = "{$127;$188}";
        private const int _quantidadeMaxLinhaPorPagina = 5;

        #region MarcaProduto
        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }
        #endregion

        #region LocalProduto
        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }
        #endregion

        #region UnidadeMedida
        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }
        #endregion

        #region Produto
        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }
        #endregion

        #region Pais
        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }
        #endregion

        #region Estado
        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }
        #endregion

        #region Cidade
        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }
        #endregion

        #region Fornecedor
        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }
        #endregion

        #region  PerfilUsuario
        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }
        #endregion

        #region Grupos de produtos

        // Abre a view com a lista acima, caso queira usar a lista.
        [Authorize]
        public ActionResult GrupoProduto()
        {
            ViewBag.QuantidadeMaxLinhaPorPagina = _quantidadeMaxLinhaPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = GrupoProdutoModel.RecuperarLista(ViewBag.PaginaAtual, _quantidadeMaxLinhaPorPagina);
            var Quant = GrupoProdutoModel.RecuperarQuantidade();
            var difQuantPaginas = (Quant % ViewBag.QuantidadeMaxLinhaPorPagina) > 0 ? 1 : 0; //Se tiver resto joga 1
            ViewBag.QuantPaginas = difQuantPaginas + (lista.Count / ViewBag.QuantidadeMaxLinhaPorPagina); //Divide a qtde de registro somando o resto.

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult GrupoProdutoPagina(int pagina)
        {
            var lista = GrupoProdutoModel.RecuperarLista(pagina, _quantidadeMaxLinhaPorPagina);
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
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            //Validação
            if (!ModelState.IsValid)
            {
                resultado = "AVISO";

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
                        resultado = "ERRO";
                    }

                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        #endregion

        #region Usuarios


        // Abre a view com a lista acima, caso queira usar a lista.
        [Authorize]
        public ActionResult Usuario()
        {
            ViewBag.SenhaPadrao = _senhaPadrao;
            ViewBag.QuantidadeMaxLinhaPorPagina = _quantidadeMaxLinhaPorPagina;
            ViewBag.PaginaAtual = 1;
            var lista = UsuarioModel.RecuperarLista(ViewBag.PaginaAtual, _quantidadeMaxLinhaPorPagina);
            var Quant = UsuarioModel.RecuperarQuantidade();

            var difQuantPaginas = (Quant % ViewBag.QuantidadeMaxLinhaPorPagina) > 0 ? 1 : 0; //Se tiver resto joga 1
            ViewBag.QuantPaginas = difQuantPaginas + (lista.Count / ViewBag.QuantidadeMaxLinhaPorPagina); //Divide a qtde de registro somando o resto.

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult UsuarioPagina(int pagina)
        {
            var lista = UsuarioModel.RecuperarLista(pagina, _quantidadeMaxLinhaPorPagina);
            return Json(lista);
        }

        //Recupera o registro para confirmar ou excluir
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int id)
        {
            return Json(UsuarioModel.RecuperarPeloId(id));
        }

        //Passa o carro no registro
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.ExcluirPeloId(id));
        }

        //Salva adicionando o item na lista
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            //Validação
            if (!ModelState.IsValid)
            {
                resultado = "AVISO";

                //lista de mensagem
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    if (model.Senha == _senhaPadrao)
                    {
                        model.Senha = "";
                    }

                    //Busca o Registro
                    var id = model.Salvar();

                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }

                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }
        #endregion

    }
}