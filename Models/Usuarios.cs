using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Nombre { get; set; }
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(70)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Rol { get; set; }
    }
}
