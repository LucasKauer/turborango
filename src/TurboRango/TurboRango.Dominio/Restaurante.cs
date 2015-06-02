using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    internal class Restaurante
    {
        /// <summary>
        /// Capacidade (lotação máxima) do restaurante
        /// </summary>
        internal int Capacidade { get; set; }
        // Int32 or Int64
        internal string Nome { get; set; }
        internal Localizacao Localizacao { get; set; }
        internal Contato Contato { get; set; }
        internal Categoria Categoria { get; set; }

    }
}
