namespace EjemploConsola.Services
{
    /// <summary>
    /// Servicio responsable de la captura y validaci�n de datos del usuario
    /// </summary>
    public class FormularioService
    {
        private readonly ValidacionService _validacionService;

        public FormularioService(ValidacionService validacionService)
        {
            _validacionService = validacionService;
        }

        public (string nombre, int edad) EjecutarFormulario()
        {
            string nombre = CapturarNombre();
            int edad = CapturarEdad();
            
            Console.WriteLine($"�Hola, {nombre}! Tienes {edad} a�os.");
            Console.WriteLine("Gracias por proporcionar tu informaci�n.");
            
            return (nombre, edad);
        }

        private string CapturarNombre()
        {
            while (true)
            {
                Console.Write("Por favor, ingresa tu nombre: ");
                string? entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("? El nombre no puede estar vac�o. Intenta de nuevo.");
                    continue;
                }

                if (_validacionService.ValidarNombre(entrada, out string nombreNormalizado))
                {
                    return nombreNormalizado;
                }
                else
                {
                    Console.WriteLine("? El nombre solo puede contener letras y espacios. Intenta de nuevo.");
                }
            }
        }

        private int CapturarEdad()
        {
            while (true)
            {
                Console.Write("Por favor, ingresa tu edad: ");
                string? entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("? La edad no puede estar vac�a. Intenta de nuevo.");
                    continue;
                }

                if (_validacionService.ValidarEdad(entrada, out int edad))
                {
                    return edad;
                }
                else
                {
                    Console.WriteLine("? La edad debe ser un n�mero v�lido entre 0 y 120. Intenta de nuevo.");
                }
            }
        }
    }
}
