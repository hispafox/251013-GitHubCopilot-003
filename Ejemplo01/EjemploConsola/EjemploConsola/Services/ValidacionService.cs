using SaludoCli.Core;

namespace EjemploConsola.Services
{
    /// <summary>
    /// Servicio de validación con implementación en C#
    /// (Integración con PersonaHelpers de VB.NET pendiente de configuración)
    /// </summary>
    public class ValidacionService
    {
        public bool ValidarNombre(string nombre, out string nombreNormalizado)
        {
            nombreNormalizado = string.Empty;
            
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return false;
            }

            // Validar que solo contiene letras y espacios
            foreach (char c in nombre)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }

            // Normalizar nombre a Title Case
            nombreNormalizado = NormalizarNombre(nombre);
            return true;
        }

        private string NormalizarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return string.Empty;

            var palabras = nombre.Trim()
                .ToLower()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < palabras.Length; i++)
            {
                if (palabras[i].Length > 0)
                {
                    palabras[i] = char.ToUpper(palabras[i][0]) + palabras[i].Substring(1);
                }
            }

            return string.Join(" ", palabras);
        }

        public bool ValidarEdad(string edadTexto, out int edad)
        {
            edad = 0;
            
            if (string.IsNullOrWhiteSpace(edadTexto))
            {
                return false;
            }

            if (!int.TryParse(edadTexto, out edad))
            {
                return false;
            }

            // Validar rango razonable de edad
            if (edad < 0 || edad > 120)
            {
                return false;
            }

            return true;
        }

        public bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Patrón básico de validación de email
            var regex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public bool ValidarFecha(string fechaTexto, out DateTime fecha)
        {
            fecha = DateTime.MinValue;
            
            // Intentar parsear en formato dd/MM/yyyy
            if (!DateTime.TryParseExact(
                fechaTexto, 
                "dd/MM/yyyy", 
                System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, 
                out fecha))
            {
                return false;
            }

            // Validar que no sea futura y que sea >= 01/01/1900
            if (fecha > DateTime.Now || fecha < new DateTime(1900, 1, 1))
            {
                return false;
            }

            return true;
        }
    }
}
