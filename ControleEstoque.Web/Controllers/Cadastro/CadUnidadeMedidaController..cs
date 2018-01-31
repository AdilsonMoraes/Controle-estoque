using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class CadUnidadeMedidaController : Controller
    {

        #region UnidadeMedida
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

    }
}