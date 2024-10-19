using Examen1;
using System;
using System.Linq;

namespace RecursosHumanos
{
    public class Menu
    {
        private RegistroEmpleados registroEmpleados;

        public Menu()
        {
            registroEmpleados = new RegistroEmpleados(100); // Capacidad máxima de 100 empleados
        }

        public void MostrarMenu()
        {
            int opcion;
            do
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("|     Menú Principal    |");
                Console.WriteLine("|1. Agregar Empleados   |");
                Console.WriteLine("|2. Consultar Empleados |");
                Console.WriteLine("|3. Modificar Empleados |");
                Console.WriteLine("|4. Borrar Empleados    |");
                Console.WriteLine("|5. Inicializar Arreglos|");
                Console.WriteLine("|6. Reportes|           |");
                Console.WriteLine("|7. Salir               |");
                Console.WriteLine("-------------------------");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarEmpleado();
                        break;
                    case 2:
                        ConsultarEmpleado();
                        break;
                    case 3:
                        ModificarEmpleado();
                        break;
                    case 4:
                        BorrarEmpleado();
                        break;
                    case 5:
                        InicializarArreglos();
                        break;
                    case 6:
                        Reportes();
                        break;
                    case 7:
                        Console.WriteLine("Saliendo del programa.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            } while (opcion != 7);
        }

        private void AgregarEmpleado()
        {
            Console.Write("Ingrese la cédula: ");
            string cedula = Console.ReadLine();
            Console.Write("Ingrese el nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la dirección: ");
            string direccion = Console.ReadLine();

            int telefono;
            do
            {
                Console.Write("Ingrese el teléfono:+506 ");
                string telefonoInput = Console.ReadLine();

                // Validar si el valor ingresado es un número entero y tiene exactamente 8 dígitos
                if (!int.TryParse(telefonoInput, out telefono) || telefonoInput.Length != 8)
                {
                    Console.WriteLine("El teléfono debe ser un número de 8 dígitos. Intente de nuevo.");
                }
                else
                {
                    // Si la validación es exitosa, salir del bucle
                    break;
                }
            } while (true);
            // Validar que el salario no exceda 10 millones
            decimal salario;
            do
            {
                Console.Write("Ingrese el salario (máximo 10 millones): ");
                if (!decimal.TryParse(Console.ReadLine(), out salario) || salario > 10000000)
                {
                    Console.WriteLine("El salario debe ser un número decimal menor o igual a 10 millones. Intente de nuevo.");
                }
                else
                {
                    break;
                }
            } while (true);

            Empleado empleado = new Empleado(cedula, nombre, direccion, telefono, salario);
            registroEmpleados.AgregarEmpleado(empleado);

            Console.WriteLine("Empleado agregado correctamente.");
        }

        private void ConsultarEmpleado()
        {
            Console.Write("Ingrese la cédula del empleado a consultar: ");
            string cedula = Console.ReadLine();
            Empleado empleado = registroEmpleados.ObtenerEmpleado(cedula);

            if (empleado != null)
            {
                Console.WriteLine($"Cédula: {empleado.Cedula}, Nombre: {empleado.Nombre}, Dirección: {empleado.Direccion}, Teléfono: {empleado.Telefono}, Salario: {empleado.Salario}");
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }

        private void ModificarEmpleado()
        {
            Console.Write("Ingrese la cédula del empleado a modificar: ");
            string cedula = Console.ReadLine();
            Empleado empleado = registroEmpleados.ObtenerEmpleado(cedula);

            if (empleado != null)
            {
                Console.Write("Ingrese el nuevo nombre: ");
                empleado.Nombre = Console.ReadLine();
                Console.Write("Ingrese la nueva dirección: ");
                empleado.Direccion = Console.ReadLine();

                // Validación del nuevo teléfono (8 dígitos)
                int nuevoTelefono;
                do
                {
                    Console.Write("Ingrese el nuevo teléfono:+506 ");
                    string telefonoInput = Console.ReadLine();

                    if (!int.TryParse(telefonoInput, out nuevoTelefono) || telefonoInput.Length != 8)
                    {
                        Console.WriteLine("El teléfono debe ser un número de 8 dígitos. Intente de nuevo.");
                    }
                    else
                    {
                        // Si es válido, asignarlo al empleado y salir del bucle
                        empleado.Telefono = nuevoTelefono;
                        break;
                    }
                } while (true);

                // Validación del nuevo salario (máximo 10 millones)
                decimal nuevoSalario;
                do
                {
                    Console.Write("Ingrese el nuevo salario (máximo 10 millones): ");
                    if (!decimal.TryParse(Console.ReadLine(), out nuevoSalario) || nuevoSalario > 10000000)
                    {
                        Console.WriteLine("El salario debe ser un número decimal menor o igual a 10 millones. Intente de nuevo.");
                    }
                    else
                    {
                        // Si es válido, asignarlo al empleado y salir del bucle
                        empleado.Salario = nuevoSalario;
                        break;
                    }
                } while (true);

                Console.WriteLine("Empleado modificado correctamente.");
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }

        private void BorrarEmpleado()
        {
            Console.Write("Ingrese la cédula del empleado a borrar: ");
            string cedula = Console.ReadLine();
            Empleado empleado = registroEmpleados.ObtenerEmpleado(cedula);

            if (empleado != null)
            {
                // Aquí se podría establecer el empleado a null, pero como estamos usando un arreglo,
                // necesitamos hacer una búsqueda y desplazar los elementos para "borrar" el empleado
                for (int i = 0; i < registroEmpleados.empleados.Length; i++)
                {
                    if (registroEmpleados.empleados[i]?.Cedula == cedula)
                    {
                        // Desplazar empleados hacia la izquierda
                        for (int j = i; j < registroEmpleados.empleados.Length - 1; j++)
                        {
                            registroEmpleados.empleados[j] = registroEmpleados.empleados[j + 1];
                        }
                        // Establecer el último empleado a null
                        registroEmpleados.empleados[registroEmpleados.empleados.Length - 1] = null;
                        Console.WriteLine("Empleado borrado correctamente.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }

        private void InicializarArreglos()
        {
            // Reiniciar el registro de empleados
            for (int i = 0; i < registroEmpleados.empleados.Length; i++)
            {
                registroEmpleados.empleados[i] = null;
            }
            Console.WriteLine("Arreglos de empleados inicializados correctamente.");
        }

        private void Reportes()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("|       ---- Reportes ----       |");
            Console.WriteLine("|1. Listar todos los empleados   |");
            Console.WriteLine("|2. Calcular promedio de salarios|");
            Console.WriteLine("----------------------------------");
            Console.Write("Seleccione una opción: ");

            int opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    ListarEmpleados();
                    break;
                case 2:
                    CalcularPromedioSalarios();
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private void ListarEmpleados()
        {
            Console.WriteLine("Lista de Empleados:");
            foreach (var empleado in registroEmpleados.empleados)
            {
                if (empleado != null)
                {
                    Console.WriteLine($"Cédula: {empleado.Cedula}, Nombre: {empleado.Nombre}, Dirección: {empleado.Direccion}, Teléfono: {empleado.Telefono}, Salario: {empleado.Salario}");
                }
            }
        }

        private void CalcularPromedioSalarios()
        {
            decimal totalSalarios = 0;
            int contadorSalarios = 0;

            foreach (var empleado in registroEmpleados.empleados)
            {
                if (empleado != null)
                {
                    totalSalarios += empleado.Salario;
                    contadorSalarios++;
                }
            }

            if (contadorSalarios > 0)
            {
                decimal promedio = totalSalarios / contadorSalarios;
                Console.WriteLine($"El promedio de salarios es: {promedio}");
            }
            else
            {
                Console.WriteLine("No hay empleados registrados para calcular el promedio.");
            }
        }

    }
}