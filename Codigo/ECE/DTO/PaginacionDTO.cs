using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECE.DTO
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int recordsPorPagina = 5;

        private readonly int cantidadMaximaPorPagina = 10;

        public int RecordsPorPagina
        {
            get { return recordsPorPagina; }
            set
            {
                recordsPorPagina = (value > cantidadMaximaPorPagina) ? cantidadMaximaPorPagina : value;//previene que el usuario mande cantidades incoherentes de registros por pág.
            }
        }
    }
}