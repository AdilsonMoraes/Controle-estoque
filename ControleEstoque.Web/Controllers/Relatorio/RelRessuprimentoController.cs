using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class RelRessuprimentoController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}