using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public class Cardapio : Entidade
    {
        public IList<Prato> MenuRestaurantes { get; set; }

        //internal void adicionaPrato(Prato prato)
        //{
        //    if(!Cardapio.Contains(prato)) {
        //        Cardapio.Add(prato);
        //    }
        //}

        //internal void removePrato(Prato prato)
        //{
        //    if (Cardapio.Contains(prato))
        //    {
        //        Cardapio.Remove(prato);
        //    }
        //}
    }
}
