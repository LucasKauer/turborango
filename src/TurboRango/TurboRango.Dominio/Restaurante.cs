using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    internal class Restaurante
    {
        private string capacidade { get; private set; }
        private string nome { get; private set; }
        private Localizacao localizacao { get; private set; }
        private Contato contato { get; private set; }
        private Categoria categoria { get; private set; }

    }
}
