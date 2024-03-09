using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models
{
    public class Producto
    {
        [Key]
        [StringLength(60)]
        [Editable(true)]
        public string Codigo { get; set; }
        [StringLength(9999)]
        public string Descripcion { get; set; }

        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal Existencia { get; set; }
    }
}
