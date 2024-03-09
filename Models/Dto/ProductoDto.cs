using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models.Dto
{
    public class ProductoDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal Existencia { get; set; }
    }
}
