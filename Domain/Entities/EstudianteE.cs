using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prueba.Entities
{
    [Table("Tbl_Estudiante")]
    public class EstudianteE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_Estudiante { get; set; }
        public string s_nombre { get; set; }
        public string s_documento { get; set; }
        public string s_correo { get; set; }

    }
}
