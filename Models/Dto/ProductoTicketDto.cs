using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models.Dto
{
    public class ProductoTicketDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal Importe { get; set; }
    }
}
