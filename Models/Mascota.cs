using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TAREA___CRUD_CON_EF.Models
{
    [Table("t_mascotas")]
    public class Mascota
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Raza { get; set; }
        public string? Color { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
    }
}