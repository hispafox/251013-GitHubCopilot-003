using EjemploConsola.Services;

namespace EjemploConsola
{
    /// <summary>
    /// Clase de compatibilidad que mantiene la interfaz original
    /// OBSOLETO: Usar los servicios en EjemploConsola.Services en su lugar
    /// </summary>
    [Obsolete("Usar PresentacionService, FormularioService y AplicacionService en su lugar")]
    public static class FormularioConsola
    {
        private static readonly PresentacionService _presentacionService = new();
        private static readonly ValidacionService _validacionService = new();
        private static readonly FormularioService _formularioService = new(_validacionService);

        public static void MostrarPresentacion2()
        {
            _presentacionService.MostrarPresentacion();
        }

        // Método consolidado - ahora usa el servicio
        public static void MostrarPresentacion()
        {
            _presentacionService.MostrarPresentacion();
        }

        public static (string nombre, string edad) EjecutarFormulario()
        {
            var (nombre, edadInt) = _formularioService.EjecutarFormulario();
            return (nombre, edadInt.ToString());
        }

        public static void FinalizarAplicacion(string nombre, string edad)
        {
            _presentacionService.EsperarTecla("Presiona cualquier tecla para salir...");
            _presentacionService.MostrarMensajeDespedida();
            // Removido Environment.Exit(0) para mejor testabilidad
        }
    }
}
