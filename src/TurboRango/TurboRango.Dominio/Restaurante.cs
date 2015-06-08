using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Restaurante : Entidade
    {
        // F12 em cima do nome da classe redireciona para a classe

        /// <summary>
        /// Capacidade (lotação máxima) do restaurante
        /// </summary>
        public int? Capacidade { get; set; }
        // Int32 or Int64
        public string Nome { get; set; }
        // virtual --> pode ser sub-escrito
        public virtual Localizacao Localizacao { get; set; }
        public virtual Contato Contato { get; set; }
        public Categoria Categoria { get; set; }

       /*
        * /// <summary>
        * /// 
        * /// </summary>
        * /// <param name="x"></param>
        * /// <returns></returns>
        * internal string Imprimir(int x)
        * {
        *     return null;
        * }
        */
    }
}
