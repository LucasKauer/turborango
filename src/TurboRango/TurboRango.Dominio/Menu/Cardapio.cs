using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public class Cardapio : Entidade
    {
        public ICollection<Prato> MenuRestaurantes { get; set; }
    }
}
