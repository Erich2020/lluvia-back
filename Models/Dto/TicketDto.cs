using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models.Dto
{
    public class TicketDto
    {
        public int Folio { get; set; }
        public string ListaProductosString { get; set; }
        public List<ProductoTicket> ListaProductos { get; set; }
        public decimal TotalArticulos { get; set; }
        public decimal TotalVenta { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public int Fk_Usuario { get; set; }
    }
}
