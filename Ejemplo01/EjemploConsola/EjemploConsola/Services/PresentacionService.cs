namespace EjemploConsola.Services
{
    /// <summary>
    /// Servicio responsable de la presentaci�n y mensajes de la aplicaci�n
    /// </summary>
    public class PresentacionService
    {
        public void MostrarPresentacion()
        {
            Console.WriteLine("Bienvenido a la aplicaci�n de ejemplo.");
            Console.WriteLine("Esta aplicaci�n demuestra las funcionalidades b�sicas de C#.");
            Console.WriteLine("�Disfruta explorando el c�digo!");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public void MostrarMensajeDespedida()
        {
            Console.WriteLine("�Hasta luego!");
            Console.WriteLine("Cerrando la aplicaci�n...");
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
