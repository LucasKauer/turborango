using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public class Cardapio : Entidade
    {
        
        public string Nome { get; set; }
        public decimal? Preco { get; set; }
        public DateTime? DateTime { get; set; }
        
        #region !Prioridade
        public Categoria Categoria { get; set; }
        public InformacaoLegal InformacaoLegal { get; set; }
        #endregion
    }
}
