using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prueba.Entities
{
    [Table("Tbl_Incripcio")]
    public class InscripcionE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_Inscripcion { get; set; }
        public int id_Estudiante { get; set; }
        public int id_materia { get; set; }
    }
}
