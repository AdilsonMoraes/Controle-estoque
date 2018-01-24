using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCodeFierst.Models
{
    public class SobreInformacoes
    {
        public int Id { get; set; }
        public string LinguagemDev { get; set; }
        public string BancoDados { get; set; }
        public string Buildversao { get; set; }
    }
}