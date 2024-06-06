using SistemaDeHorario.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeHorario.Repositories
{
    public class HorarioRepository
    {
        SQLiteConnection conexion;
        public HorarioRepository()
        {
            conexion = new("horario.sqlite");
            conexion.CreateTable<Horario>();
        }

        public IEnumerable<Horario> GetLunes()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Lunes").OrderBy(x => x.HoraInicio);
        }
        public IEnumerable<Horario> GetMartes()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Martes").OrderBy(x => x.HoraInicio);
        }
        public IEnumerable<Horario> GetMiercoles()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Miercoles").OrderBy(x => x.HoraInicio);
        }
        public IEnumerable<Horario> GetJueves()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Jueves").OrderBy(x => x.HoraInicio);
        }
        public IEnumerable<Horario> GetViernes()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Viernes").OrderBy(x => x.HoraInicio);
        }
        public IEnumerable<Horario> GetSabado()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Sabado").OrderBy(x => x.HoraInicio);
        }
        public IEnumerable<Horario> GetDomingo()
        {
            return conexion.Table<Horario>().Where(x => x.Dia == "Domingo").OrderBy(x => x.HoraInicio);
        }

        public void Insert(Horario horario)
        {
            conexion.Insert(horario);
        }

        public void Update(Horario horario)
        {
            conexion.Update(horario);
        }

        public void Delete(Horario horario)
        {
            conexion.Delete(horario);
        }
    }
}
