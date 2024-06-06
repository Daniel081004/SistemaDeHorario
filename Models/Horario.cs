using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeHorario.Models
{
    [Table("horario")]
    public class Horario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string NombreAsignatura { get; set; } = "";

        [MaxLength(100)]
        public string NombreMaestro { get; set; } = "";

        [MaxLength(50)]
        public string Aula { get; set; } = "";

        [MaxLength(255)]
        public string Descripcion { get; set; } = "";
        [NotNull, MaxLength(10)]
        public string Dia { get; set; } = "";
        public int HoraInicio { get; set; }
        public int HoraFin { get; set; }
    }
}
