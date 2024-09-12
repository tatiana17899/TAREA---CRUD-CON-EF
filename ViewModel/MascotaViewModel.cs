using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAREA___CRUD_CON_EF.Models;

namespace TAREA___CRUD_CON_EF.ViewModel
{
    public class MascotaViewModel
    {
        public Mascota? FormMascota { get; set; }
        public List<Mascota>? ListMascota { get; set; }
    }
}