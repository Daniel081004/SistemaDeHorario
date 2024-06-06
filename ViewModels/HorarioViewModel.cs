using CommunityToolkit.Mvvm.Input;
using SistemaDeHorario.Models;
using SistemaDeHorario.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaDeHorario.ViewModels
{
    public class HorarioViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Horario> Lunes { get; set; } = new();
        public ObservableCollection<Horario> Martes { get; set; } = new();
        public ObservableCollection<Horario> Miercoles { get; set; } = new();
        public ObservableCollection<Horario> Jueves { get; set; } = new();
        public ObservableCollection<Horario> Viernes { get; set; } = new();
        public ObservableCollection<Horario> Sabado { get; set; } = new();
        public ObservableCollection<Horario> Domingo { get; set; } = new();

        public ICommand VerAgregarCommand { get; set; }
        public ICommand VerHorarioCommand { get; set; }
        public ICommand AgregarClaseCommand {  get; set; }
        public ICommand AgregarActividadCommand { get; set; }
        public ICommand EliminarEditarCommand {  get; set; }
        public ICommand ModoEliminarCommand { get; set; }
        public ICommand ModoEditarCommand { get; set; }
        public ICommand SeleccionClaseCommand { get; set; }
        public ICommand SeleccionActividadCommand { get; set; }
        public ICommand EditarClaseCommand { get; set; }
        public ICommand EditarActividadCommand { get; set; }

        public HorarioRepository Repository = new();

        public Horario Horario { get; set; } = new();

        public string Vista { get; set; } = "Main";
        public string ModoInteraccion { get; set; } = "";
        public string Error { get; set; } = "";
        public string ActividadClase { get; set; } = "";

        public HorarioViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregar);
            VerHorarioCommand = new RelayCommand(VerHorario);
            AgregarActividadCommand = new RelayCommand(AgregarActividad);
            AgregarClaseCommand = new RelayCommand(AgregarClase);
            ModoEliminarCommand = new RelayCommand(ModoEliminar);
            ModoEditarCommand = new RelayCommand(ModoEditar);
            SeleccionClaseCommand = new RelayCommand(SelccionClase);
            SeleccionActividadCommand = new RelayCommand(SeleccionActividad);
            EliminarEditarCommand = new RelayCommand<Horario>(EliminarEditar);
            EditarClaseCommand = new RelayCommand(EditarClase);
            EditarActividadCommand = new RelayCommand(EditarActividad);
            MostrarDatos();
        }

        private void EditarActividad()
        {
            if (Horario != null)
            {
                if (Horario.Descripcion == "")
                {
                    Error += "Escribe el nombre de la actividad\n";
                }
                if (Horario.Dia == "")
                {
                    Error += "Elige un dia\n";
                }
                if (Horario.HoraInicio < 0 || Horario.HoraFin <= 0)
                {
                    Error += "Las horas no pueden ser negativas\n";
                }
                if (Horario.HoraInicio > Horario.HoraFin)
                {
                    Error += "La hora final es mayor a la hora inicial\n";
                }
                if (Horario.HoraInicio == Horario.HoraFin)
                {
                    Error += "La actividad no dura ni siquiera una hora\n";
                }

                ObservableCollection<Horario> Lista;

                if (Horario.Dia == "Lunes")
                {
                    Lista = Lunes;
                }
                else if (Horario.Dia == "Martes")
                {
                    Lista = Martes;
                }
                else if (Horario.Dia == "Miercoles")
                {
                    Lista = Miercoles;
                }
                else if (Horario.Dia == "Jueves")
                {
                    Lista = Jueves;
                }
                else if (Horario.Dia == "Viernes")
                {
                    Lista = Viernes;
                }
                else if (Horario.Dia == "Sabado")
                {
                    Lista = Sabado;
                }
                else
                {
                    Lista = Domingo;
                }

                foreach (var horario in Lista)
                {
                    if (SeSolapan(horario, Horario))
                    {
                        if(horario.Id != Horario.Id)
                        {
                            Error += "Ya hay una clase o actividad en que  se solapa en el horario seleccionado";
                        }
                    }
                }
                PropertyChanged?.Invoke(this, new(null));
                if (Error == "")
                {
                    Repository.Update(Horario);
                    VerHorario();
                    MostrarDatos();
                }
                else
                {
                    Error = "";
                }
            }
        }

        private void EditarClase()
        {
            if (Horario != null)
            {
                if (Horario.NombreAsignatura == "")
                {
                    Error += "Escribe el nombre de la asignatura\n";
                }
                if (Horario.NombreMaestro == "")
                {
                    Error += "Escribe el nombre del maestro\n";
                }
                if (Horario.Aula == "")
                {
                    Error += "Escribe la aula asiganada para esta clase\n";
                }
                if (Horario.Dia == "")
                {
                    Error += "Elige un dia\n";
                }
                if (Horario.HoraInicio < 0 || Horario.HoraFin <= 0)
                {
                    Error += "Las horas no pueden ser negativas\n";
                }
                if (Horario.HoraInicio > Horario.HoraFin)
                {
                    Error += "La hora final es mayor a la hora inicial\n";
                }
                if (Horario.HoraInicio == Horario.HoraFin)
                {
                    Error += "La clase no dura ni siquiera una hora\n";
                }

                ObservableCollection<Horario> Lista;

                if (Horario.Dia == "Lunes")
                {
                    Lista = Lunes;
                }
                else if (Horario.Dia == "Martes")
                {
                    Lista = Martes;
                }
                else if (Horario.Dia == "Miercoles")
                {
                    Lista = Miercoles;
                }
                else if (Horario.Dia == "Jueves")
                {
                    Lista = Jueves;
                }
                else if (Horario.Dia == "Viernes")
                {
                    Lista = Viernes;
                }
                else if (Horario.Dia == "Sabado")
                {
                    Lista = Sabado;
                }
                else
                {
                    Lista = Domingo;
                }

                foreach (var horario in Lista)
                {
                    if (SeSolapan(horario, Horario))
                    {
                        if(horario.Id != Horario.Id)
                        {
                            Error += "Ya hay una clase o actividad en que  se solapa en el horario seleccionado";
                        }
                    }
                }
                PropertyChanged?.Invoke(this, new(null));
                if (Error == "")
                {
                    Repository.Update(Horario);
                    VerHorario();
                    MostrarDatos();
                }
                else
                {
                    Error = "";
                }
            }
        }

        private void EliminarEditar(Horario? h)
        {
            if(ModoInteraccion == "Eliminar")
            {
                if(h != null)
                {
                    Repository.Delete(h);
                    MostrarDatos();
                    PropertyChanged?.Invoke(this, new(null));
                }
            }
            else if (ModoInteraccion == "Editar")
            {
                if(h != null)
                {
                    Vista = "Editar";
                    ModoInteraccion = "";
                    Horario = new()
                    {
                        Aula = h.Aula,
                        NombreAsignatura = h.NombreAsignatura,
                        Descripcion = h.Descripcion,
                        Dia = h.Dia,
                        HoraFin = h.HoraFin,
                        HoraInicio = h.HoraInicio,
                        Id = h.Id,
                        NombreMaestro = h.NombreMaestro
                    };
                    ActividadClase = Horario.NombreAsignatura == "" ? "Actividad" : "Clase";
                    PropertyChanged?.Invoke(this, new(null));
                }
            }
        }

        private void SeleccionActividad()
        {
            ActividadClase = "Actividad";
            PropertyChanged?.Invoke(this, new(null));
        }
        private void SelccionClase()
        {
            ActividadClase = "Clase";
            PropertyChanged?.Invoke(this, new(null));
        }

        private void ModoEditar()
        {
            if (ModoInteraccion != "Editar")
            {
                ModoInteraccion = "Editar";
            }
            else
            {
                ModoInteraccion = "";
            }
            PropertyChanged?.Invoke(this, new(null));
        }
        private void ModoEliminar()
        {
            if(ModoInteraccion != "Eliminar")
            {
                ModoInteraccion = "Eliminar";
            }
            else
            {
                ModoInteraccion = "";
            }
            PropertyChanged?.Invoke(this, new(null));
        }
        private void AgregarClase()
        {
            if(Horario != null)
            {
                if(Horario.NombreAsignatura == "")
                {
                    Error += "Escribe el nombre de la asignatura\n";
                }
                if(Horario.NombreMaestro == "")
                {
                    Error += "Escribe el nombre del maestro\n";
                }
                if(Horario.Aula == "")
                {
                    Error += "Escribe la aula asiganada para esta clase\n";
                }
                if(Horario.Dia == "")
                {
                    Error += "Elige un dia\n";
                }
                if(Horario.HoraInicio < 0 || Horario.HoraFin <= 0)
                {
                    Error += "Las horas no pueden ser negativas\n";
                }
                if(Horario.HoraInicio > Horario.HoraFin)
                {
                    Error += "La hora final es mayor a la hora inicial\n";
                }
                if(Horario.HoraInicio == Horario.HoraFin)
                {
                    Error += "La clase no dura ni siquiera una hora\n";
                }

                ObservableCollection<Horario> Lista;

                if(Horario.Dia == "Lunes")
                {
                    Lista = Lunes;
                }
                else if(Horario.Dia == "Martes")
                {
                    Lista = Martes;
                }
                else if (Horario.Dia == "Miercoles")
                {
                    Lista = Miercoles;
                }
                else if (Horario.Dia == "Jueves")
                {
                    Lista = Jueves;
                }
                else if (Horario.Dia == "Viernes")
                {
                    Lista = Viernes;
                }
                else if (Horario.Dia == "Sabado")
                {
                    Lista = Sabado;
                }
                else
                {
                    Lista = Domingo;
                }

                foreach (var horario in Lista)
                {
                    if (SeSolapan(horario, Horario))
                    {
                        Error += "Ya hay una clase o actividad en que  se solapa en el horario seleccionado";
                    }
                }
                PropertyChanged?.Invoke(this, new(null));
                if (Error == "")
                {
                    Repository.Insert(Horario);
                    VerHorario();
                    MostrarDatos();
                }
                else
                {
                    Error = "";
                }
            }
        }
        private void AgregarActividad()
        {
            if (Horario != null)
            {
                if (Horario.Descripcion == "")
                {
                    Error += "Escribe el nombre de la actividad\n";
                }
                if (Horario.Dia == "")
                {
                    Error += "Elige un dia\n";
                }
                if (Horario.HoraInicio < 0 || Horario.HoraFin <= 0)
                {
                    Error += "Las horas no pueden ser negativas\n";
                }
                if (Horario.HoraInicio > Horario.HoraFin)
                {
                    Error += "La hora final es mayor a la hora inicial\n";
                }
                if (Horario.HoraInicio == Horario.HoraFin)
                {
                    Error += "La actividad no dura ni siquiera una hora\n";
                }

                ObservableCollection<Horario> Lista;

                if (Horario.Dia == "Lunes")
                {
                    Lista = Lunes;
                }
                else if (Horario.Dia == "Martes")
                {
                    Lista = Martes;
                }
                else if (Horario.Dia == "Miercoles")
                {
                    Lista = Miercoles;
                }
                else if (Horario.Dia == "Jueves")
                {
                    Lista = Jueves;
                }
                else if (Horario.Dia == "Viernes")
                {
                    Lista = Viernes;
                }
                else if (Horario.Dia == "Sabado")
                {
                    Lista = Sabado;
                }
                else
                {
                    Lista = Domingo;
                }

                foreach (var horario in Lista)
                {
                    if (SeSolapan(horario, Horario))
                    {
                        Error += "Ya hay una clase o actividad en que  se solapa en el horario seleccionado";
                    }
                }
                PropertyChanged?.Invoke(this, new(null));
                if (Error == "")
                {
                    Repository.Insert(Horario);
                    VerHorario();
                    MostrarDatos();
                }
                else
                {
                    Error = "";
                }
            }
        }
        private bool SeSolapan(Horario h1, Horario h2)
        {
            return h1.HoraInicio < h2.HoraFin && h2.HoraInicio < h1.HoraFin;
        }
        private void VerHorario()
        {
            Vista = "Main";
            ActividadClase = "";
            Actualizar(Vista);
            PropertyChanged?.Invoke(this, new(null));
            MostrarDatos();
        }
        private void VerAgregar()
        {
            ModoInteraccion = "";
            PropertyChanged?.Invoke(this, new(null));
            Horario = new();
            Error = "";
            Vista = "Agregar";
            Actualizar(Vista);
            Actualizar(Error);
            PropertyChanged?.Invoke(this, new(nameof(Horario)));
        }

        private void MostrarDatos()
        {
            Lunes.Clear();
            Martes.Clear();
            Miercoles.Clear();
            Jueves.Clear();
            Viernes.Clear();
            Sabado.Clear();
            Domingo.Clear();
            var datos = Repository.GetLunes();
            foreach (var a in datos)
            {
                Lunes.Add(a);
            }
            datos = Repository.GetMartes();
            foreach (var a in datos)
            {
                Martes.Add(a);
            }
            datos = Repository.GetMiercoles();
            foreach (var a in datos)
            {
                Miercoles.Add(a);
            }
            datos = Repository.GetJueves();
            foreach (var a in datos)
            {
                Jueves.Add(a);
            }
            datos = Repository.GetViernes();
            foreach (var a in datos)
            {
                Viernes.Add(a);
            }
            datos = Repository.GetSabado();
            foreach (var a in datos)
            {
                Sabado.Add(a);
            }
            datos = Repository.GetDomingo();
            foreach (var a in datos)
            {
                Domingo.Add(a);
            }
        }

        private void Actualizar(string? nombre = null)
        {
            PropertyChanged?.Invoke(this, new(nameof(nombre)));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
