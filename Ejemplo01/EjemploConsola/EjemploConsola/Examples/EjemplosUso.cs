using EjemploConsola.Services;

namespace EjemploConsola.Examples
{
    /// <summary>
    /// Ejemplos de uso avanzado de los servicios refactorizados
    /// Este archivo demuestra c�mo usar los servicios de manera flexible
    /// </summary>
    public class EjemplosUso
    {
        /// <summary>
        /// Ejemplo 1: Uso b�sico con todos los servicios
        /// </summary>
        public static void EjemploBasico()
        {
            var presentacion = new PresentacionService();
            var validacion = new ValidacionService();
            var formulario = new FormularioService(validacion);
            var aplicacion = new AplicacionService(presentacion, formulario);

            aplicacion.Ejecutar();
        }

        /// <summary>
        /// Ejemplo 2: Uso independiente de validaciones
        /// </summary>
        public static void EjemploValidaciones()
        {
            var validacion = new ValidacionService();

            // Validar nombre
            if (validacion.ValidarNombre("juan p�rez", out string nombreNormalizado))
            {
                Console.WriteLine($"Nombre v�lido y normalizado: {nombreNormalizado}");
                // Output: "Juan P�rez"
            }

            // Validar edad
            if (validacion.ValidarEdad("25", out int edad))
            {
                Console.WriteLine($"Edad v�lida: {edad}");
            }

            // Validar email
            if (validacion.ValidarEmail("usuario@ejemplo.com"))
            {
                Console.WriteLine("Email v�lido");
            }

            // Validar fecha
            if (validacion.ValidarFecha("15/06/1990", out DateTime fecha))
            {
                Console.WriteLine($"Fecha v�lida: {fecha:dd/MM/yyyy}");
            }
        }

        /// <summary>
        /// Ejemplo 3: Crear un flujo personalizado
        /// </summary>
        public static void EjemploPersonalizado()
        {
            var presentacion = new PresentacionService();
            var validacion = new ValidacionService();

            presentacion.MostrarPresentacion();

            // Captura manual con validaci�n
            Console.Write("Ingresa tu email: ");
            string? email = Console.ReadLine();

            if (email != null && validacion.ValidarEmail(email))
            {
                Console.WriteLine($"? Email {email} registrado correctamente");
            }
            else
            {
                Console.WriteLine("? Email inv�lido");
            }

            presentacion.EsperarTecla();
        }

        /// <summary>
        /// Ejemplo 4: Testing de validaciones sin UI
        /// </summary>
        public static bool TestValidaciones()
        {
            var validacion = new ValidacionService();
            bool todosPasaron = true;

            // Test 1: Nombre con n�meros debe fallar
            todosPasaron &= !validacion.ValidarNombre("Juan123", out _);

            // Test 2: Edad negativa debe fallar
            todosPasaron &= !validacion.ValidarEdad("-5", out _);

            // Test 3: Edad mayor a 120 debe fallar
            todosPasaron &= !validacion.ValidarEdad("150", out _);

            // Test 4: Email sin @ debe fallar
            todosPasaron &= !validacion.ValidarEmail("emailinvalido.com");

            // Test 5: Nombre v�lido debe pasar
            todosPasaron &= validacion.ValidarNombre("Mar�a L�pez", out string nombre);
            todosPasaron &= nombre == "Mar�a L�pez";

            // Test 6: Edad v�lida debe pasar
            todosPasaron &= validacion.ValidarEdad("30", out int edad);
            todosPasaron &= edad == 30;

            return todosPasaron;
        }
    }
}
