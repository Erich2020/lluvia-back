
using System.ComponentModel.DataAnnotations;


namespace TempporalWS.Models
{
    public class ProductoTicket
    {
        [Key]
        [StringLength(60)]
        [Editable(true)]
        public string Codigo { get; set; }
        [StringLength(9999)]
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal Importe { get; set; }
    }
}
    