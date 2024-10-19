
using System;
using System.Linq;

namespace RecursosHumanos
{
    public class Empleado
    {
        // Atributos
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public decimal Salario { get; set; }

        // Constructor
        public Empleado(string cedula, string nombre, string direccion, int telefono, decimal salario)
        {
            Cedula = cedula;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Salario = salario;
        }
    }

    public class RegistroEmpleados
    {
        public Empleado[] empleados;
        private int contador;

        public RegistroEmpleados(int capacidad)
        {
            empleados = new Empleado[capacidad];
            contador = 0;
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            if (contador < empleados.Length)
            {
                empleados[contador] = empleado;
                contador++;
            }
            else
            {
                Console.WriteLine("No se pueden agregar más empleados. Capacidad máxima alcanzada.");
            }
        }

        public Empleado ObtenerEmpleado(string cedula)
        {
            return empleados.FirstOrDefault(e => e.Cedula == cedula);
        }
    }

}