using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    internal class Localizacao
    {
        private string bairro { get; private set; }
        private double latitude { get; private set; }
        private string logradouro { get; private set; }
        private int longitude { get; private set; }
    }
}
