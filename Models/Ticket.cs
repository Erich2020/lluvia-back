using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models
{
    public class Ticket
    {
        [Key]
        public int Folio { get; set; }
        [StringLength(9999)]
        public string ListaProductosString { get; set; }
        public decimal TotalArticulos { get; set; }
        public decimal TotalVenta { get; set; }
        [StringLength(15)]
        public string Fecha { get; set; }
        [StringLength(15)]
        public string Hora { get; set; }
        public int Fk_Usuario { get; set; }
    }
}
