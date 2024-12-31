using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TsaakAPI.Entities
{
    public class VMCatalog
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }
    }
}