using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestor_de_Tareas
{
	class Fabrizio
	{
		static Dictionary<Fecha, Tarea> gestor = new Dictionary<Fecha, Tarea>();

		public class Fecha
		{
			public int Día { get; set; }
			public int Mes { get; set; }
			public int Año { get; set; }

			public Fecha(int dia, int mes, int año)
			{
				Día = dia;
				Mes = mes;
				Año = año;

				//Validaciones del Constructor
				if (dia < 1 || dia > 31)
				{
					throw new ArgumentException("Día no válido.");
				}
				if (mes < 1 || mes > 12)
				{
					throw new ArgumentException("Mes no válido.");
				}
				if (año < 1)
				{
					throw new ArgumentException("Año no válido.");
				}
			}
		}

		public class Tarea
		{
			public Fecha Fecha { get; set; }
			public string Descripción { get; set; }

			public Tarea(Fecha fecha, string descripcion)
			{
				Fecha = fecha;
				Descripción = descripcion;
			}
		}

		static int numTarea = 1;

		static void Main(string[] args)
		{
			menu();
		}

		static void menu()
		{
			Console.Clear();
			Console.WriteLine("/----------Menú de Opciones----------/\n");
			Console.WriteLine("1. Agregar Tarea.");
			Console.WriteLine("2. Eliminar Tarea.");
			Console.WriteLine("3. Editar Tarea.");
			Console.WriteLine("4. Ver Lista de Tareas.");
			Console.WriteLine("5. Cerrar Programa.\n");

			Console.Write("Elija una opción: ");
			int opción = int.Parse(Console.ReadLine());

			switch (opción)
			{
				case 1:
					AgregarTarea();
					break;
				case 2:
					EliminarTarea();
					break;
				case 3:
					EditarTarea();
					break;
				case 4:
					VerTareas();
					break;
				default:
					Console.WriteLine("Cerrando Programa...");
					return;
			}
		}

		static void AgregarTarea()
		{
			Console.Write("\nIngrese día, mes y año de su Tarea separados por espacios: ");
			string entrada = Console.ReadLine();
			string[] inputFecha = entrada.Split(' ');

			int dia = Convert.ToInt16(inputFecha[0]);
			int mes = Convert.ToInt16(inputFecha[1]);
			int año = Convert.ToInt16(inputFecha[2]);

			Console.Write("\nIngrese descripción de su Tarea: ");
			string descripcion = Console.ReadLine();

			Fecha fecha = new Fecha(dia, mes, año);
			Tarea tarea = new Tarea(fecha, descripcion);

			gestor.Add(fecha, tarea);

			Console.WriteLine($"\nLa tarea fue agregada con éxito, Número de Tarea: {numTarea}");

			volver();
		}

		static void EliminarTarea()
		{
			Console.Write("\nIngrese el número de la tarea que desea eliminar: ");
			int numTareaEliminar = int.Parse(Console.ReadLine());

			// Obtener la tarea seleccionada
			Tarea tareaEliminar = gestor.ElementAt(numTareaEliminar - 1).Value;

			//Eliminar Tarea
			gestor.Remove(tareaEliminar.Fecha);
			Console.WriteLine("\nTarea eliminada con éxito.");

			volver();
		}

		static void EditarTarea()
		{
			if (gestor.Count == 0)
			{
				Console.WriteLine("\nNo hay tareas para Editar.");
				volver();
			}

			Console.Write("\nElija el número de la tarea que desea editar: ");
			if (int.TryParse(Console.ReadLine(), out int numtareaEditar) && numTarea > 0 && numTarea <= gestor.Count)
			{
				var tareaEditar = gestor.ElementAt(numtareaEditar - 1); //Obtiene el par clave-valor en la posición específicada
				gestor.Remove(tareaEditar.Key);

				Console.Write("\nIngrese nuevo día, mes y año de su Tarea separado por espacios: ");
				string entrada = Console.ReadLine();
				string[] inputFecha = entrada.Split(' ');

				if (inputFecha.Length == 3 &&
					int.TryParse(inputFecha[0], out int dia) &&
					int.TryParse(inputFecha[1], out int mes) &&
					int.TryParse(inputFecha[2], out int año))
				{
					Console.Write("\nIngrese nueva descripción de su Tarea: ");
					string descripcion = Console.ReadLine();

					Fecha nuevaFecha = new Fecha(dia, mes, año);
					Tarea nuevaTarea = new Tarea(nuevaFecha, descripcion);

					// Agregar la tarea actualizada al diccionario gestor
					gestor.Add(nuevaFecha, nuevaTarea);

					Console.WriteLine("\nTarea editada con éxito.");
				}
				else
				{
					Console.WriteLine("\nFecha no válida. La tarea no fue editada.");
					// Reinsertar la tarea anterior en caso de entrada no válida
					gestor.Add(tareaEditar.Key, tareaEditar.Value);
				}
			}
			else
			{
				Console.WriteLine("\nNúmero de tarea no válido. Por favor intente de nuevo.");
			}

			volver();
		}

		static void VerTareas()
		{
			foreach (var ver in gestor)
			{
				Console.WriteLine($"\n{numTarea}- Fecha: {ver.Key.Día}/{ver.Key.Mes}/{ver.Key.Año}          Tarea: {ver.Value.Descripción}");
				numTarea++;
			}

			if (gestor.Count == 0)
			{
				Console.WriteLine("\nNo hay tareas registradas.");
			}

			volver();
		}
		static void volver()
		{
			Console.Write("\n¿Desea volver al menú de opciones? S/N: ");
			char volver = char.Parse(Console.ReadLine());

			if (char.ToUpper(volver) == 'S')
			{
				menu();
			}
			else
			{
				return;
			}
		}
	}
}