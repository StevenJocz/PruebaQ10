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
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }

        public static EstudianteDTOs CrearDTO(EstudianteE estudianteE)
        {
            return new EstudianteDTOs
            {
                Id = estudianteE.id_Estudiante,
                Nombre = estudianteE.s_nombre,
                Documento = estudianteE.s_documento,
                Correo = estudianteE.s_correo

            };
        }

        public static EstudianteE CrearE(EstudianteDTOs estudianteDTOs)
        {
            return new EstudianteE
            {
                id_Estudiante = estudianteDTOs.Id,
                s_nombre = estudianteDTOs.Nombre,
                s_documento = estudianteDTOs.Documento,
                s_correo = estudianteDTOs.Correo
            };
        }
    }
}
