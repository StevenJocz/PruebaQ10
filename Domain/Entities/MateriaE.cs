using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prueba.Entities
{
    [Table("Tbl_Materia")]
    public class MateriaE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_materia { get; set; }
        public string s_nombre { get; set; }
        public string s_codigo { get; set; }
        public int i_creditos { get; set; }

    }
}
