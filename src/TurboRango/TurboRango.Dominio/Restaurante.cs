﻿namespace TurboRango.Dominio
{
    public class Restaurante : Entidade
    {
        /// <summary>
        /// Capacidade (lotação máxima) do restaurante.
        /// </summary>
        public int? Capacidade { get; set; }
        // Int32 or Int64
        public string Nome { get; set; }
        // virtual --> pode ser sub-escrito
        public Contato Contato { get; set; }
        public Localizacao Localizacao { get; set; }
        public Categoria Categoria { get; set; }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal string Imprimir(int x)
        {
            return null;
        }
        */
    }
}
