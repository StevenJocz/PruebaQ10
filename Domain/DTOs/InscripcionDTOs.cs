using Domain.Prueba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prueba.DTOs
{
    public class InscripcionDTOs
    {
        public int id_Inscripcion { get; set; }
        public int id_Estudiante { get; set; }
        public int id_materia { get; set; }

        public static InscripcionDTOs CrearDTOs(InscripcionE inscripcionE)
        {
            return new InscripcionDTOs
            {
                id_Inscripcion = inscripcionE.id_Inscripcion,
                id_Estudiante = inscripcionE.id_Estudiante,
                id_materia = inscripcionE.id_materia,

            };
        }

        public static InscripcionE CrearE(InscripcionDTOs inscripcionDTOs)
        {
            return new InscripcionE
            {
                id_Inscripcion = inscripcionDTOs.id_Inscripcion,
                id_Estudiante = inscripcionDTOs.id_Estudiante,
                id_materia = inscripcionDTOs.id_materia,

            };
        }
    }

  
}
