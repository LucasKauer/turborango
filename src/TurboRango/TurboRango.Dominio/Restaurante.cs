using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    internal class Restaurante
    {
        private int Capacidade { get; set; }
        private string Nome { get; set; }
        private Localizacao Localizacao { get; set; }
        private Contato Contato { get; set; }
        private Categoria Categoria { get; set; }

    }
}
