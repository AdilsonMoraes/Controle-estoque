using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class CadMarcaProdutoController : Controller
    {

        #region MarcaProduto
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

     }
}