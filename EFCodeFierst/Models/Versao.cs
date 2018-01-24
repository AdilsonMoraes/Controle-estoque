using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCodeFierst.Models
{
    public class Versao
    {
        public int Id { get; set; }
        public string LinguagemDev { get; set; }
        public string BancoDados { get; set; }
        public string BuildVersao { get; set; }
    }
}