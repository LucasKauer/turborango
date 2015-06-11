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
        [Description("Buffet Livre")]
        BuffetLivre,
        [Description("Rodízio")]
        Rodizio,
        [Description("Comida Japonesa")]
        ComidaJaponesa,
        [Description("Comida Mexicana")]
        ComidaMexicana,
        [Description("Lanche")]
        Lanche,
        [Description("Bebida")]
        Bebida,
        [Description("Acompanhamento")]
        Acompanhamento,
        [Description("Sobremesa")]
        Sobremesas,
        [Description("Outro")]
        Outro
    }
}
