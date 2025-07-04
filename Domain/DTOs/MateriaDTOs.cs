using Domain.Prueba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prueba.DTOs
{
    public class MateriaDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public int creditos { get; set; }

        public MateriaDTOs CrearDTO(MateriaE materiaE)
        {
            return new MateriaDTOs
            {
                id = materiaE.id_materia,
                nombre = materiaE.s_nombre,
                codigo = materiaE.s_codigo,
                creditos = materiaE.i_creditos
            };
        }

        public MateriaE CrearE(MateriaDTOs materiaDTOs)
        {
            return new MateriaE
            {
                id_materia = materiaDTOs.id,
                s_nombre = materiaDTOs.nombre,
                s_codigo = materiaDTOs.codigo,
                i_creditos = materiaDTOs.creditos,

            };
        }
    }
}
