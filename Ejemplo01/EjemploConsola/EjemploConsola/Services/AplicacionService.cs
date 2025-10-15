namespace EjemploConsola.Services
{
    /// <summary>
    /// Servicio responsable de gestionar el ciclo de vida de la aplicación
    /// </summary>
    public class AplicacionService
    {
        private readonly PresentacionService _presentacionService;
        private readonly FormularioService _formularioService;
        private bool _debeTerminar = false;

        public AplicacionService(
            PresentacionService presentacionService,
            FormularioService formularioService)
        {
            _presentacionService = presentacionService;
            _formularioService = formularioService;
        }

        public void Ejecutar()
        {
            _presentacionService.MostrarPresentacion();
            
            var (nombre, edad) = _formularioService.EjecutarFormulario();
            
            Finalizar(nombre, edad);
        }

        public void Finalizar(string nombre, int edad)
        {
            _presentacionService.EsperarTecla("Presiona cualquier tecla para salir...");
            _presentacionService.MostrarMensajeDespedida();
            _debeTerminar = true;
        }

        public bool DebeTerminar => _debeTerminar;
    }
}
