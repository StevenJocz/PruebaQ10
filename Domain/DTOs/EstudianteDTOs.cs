using Domain.Prueba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prueba.DTOs
{
    public class EstudianteDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string documento { get; set; }
        public string correo { get; set; }

        public static EstudianteDTOs CrearDTO(EstudianteE estudianteE)
        {
            return new EstudianteDTOs
            {
                id = estudianteE.id_Estudiante,
                nombre = estudianteE.s_nombre,
                documento = estudianteE.s_documento,
                correo = estudianteE.s_correo

            };
        }

        public static EstudianteE CrearE(EstudianteDTOs estudianteDTOs)
        {
            return new EstudianteE
            {
                id_Estudiante = estudianteDTOs.id,
                s_nombre = estudianteDTOs.nombre,
                s_documento = estudianteDTOs.documento,
                s_correo = estudianteDTOs.correo
            };
        }
    }
}
