using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Controllers
{
    public class GlobalMsg
    {
        public enum OptionErro
        {
            OK = 1,
            ERRO = 2,
            AVISO = 3
        }

        public static string RetornaMsg(int param)
        {
            string msg = "";
            switch (param)
            {
                case 1:
                    msg = "OK";
                    break;
                case 2:
                    msg = "ERRO";
                    break;
                default:
                    msg = "AVISO";
                    break;
            }

            return msg;
        }

    }
}