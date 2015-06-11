using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public class Prato : Entidade
    {
        public string Nome { get; set; }
        public Tipo Tipo { get; set; }
        public DateTime DateTime { get; set; }
        public string InformacaoExtra { get; set; }
        public InformacaoLegal InformacaoLegal { get; set; }
        public decimal Preco { get; set; }
        public string UrlImagem { get; set; }

    }
}
