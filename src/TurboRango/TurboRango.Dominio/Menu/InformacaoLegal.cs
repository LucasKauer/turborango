using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public enum InformacaoLegal
    {
        [Description("Horrível")]
        Horrivel,
        [Description("Ruim")]
        Ruim,
        [Description("Razoável")]
        Razoavel,
        [Description("Muito Bom")]
        MuitoBom,
        [Description("Excelente")]
        Excelente,
        [Description("Iluminatti")]
        Iluminatti
    }
}
