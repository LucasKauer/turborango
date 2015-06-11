using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    public enum Tipo
    {
        [Description("Prato Pronto")]
        PratoPronto,
        [Description("Buffet")]
        Buffet,
        [Description("Rodízio")]
        Rodizio,
        [Description("Lanche")]
        Lanche,
        [Description("Bebida")]
        Bebida,
        [Description("Acompanhamento")]
        Acompanhamento,
        [Description("Sobremesa")]
        Sobremesa,
        [Description("Outro")]
        Outro
    }
}
