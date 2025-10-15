namespace EjemploConsola.Services
{
    /// <summary>
    /// Servicio responsable de la presentación y mensajes de la aplicación
    /// </summary>
    public class PresentacionService
    {
        public void MostrarPresentacion()
        {
            Console.WriteLine("Bienvenido a la aplicación de ejemplo.");
            Console.WriteLine("Esta aplicación demuestra las funcionalidades básicas de C#.");
            Console.WriteLine("¡Disfruta explorando el código!");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public void MostrarMensajeDespedida()
        {
            Console.WriteLine("¡Hasta luego!");
            Console.WriteLine("Cerrando la aplicación...");
            System.Threading.Thread.Sleep(2000);
        }

        public void EsperarTecla(string mensaje = "Presiona cualquier tecla para continuar...")
        {
            Console.WriteLine(mensaje);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
