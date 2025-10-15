// Descripción: Programa de consola en C# que muestra un saludo y espera una entrada del usuario.
// Autor: Tu Nombre
// Objetivo: Crear un formulario de consola simple que interactúe con el usuario.
//           El formulario debe mostrar un mensaje de bienvenida y esperar a que el usuario presione una tecla para continuar.
//           Pediremos información adicional al usuario para hacer el ejemplo más interactivo.
//           Pediremos sus datos personales y los mostraremos en pantalla.
// Versión: 2.0 - Refactorizado con arquitectura de servicios y validaciones
// Fecha: 2024-06-15

using EjemploConsola.Services;

// Asegura que la consola use UTF-8 para mostrar correctamente acentos y signos como ¡, ñ, etc.
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// Configurar servicios con inyección de dependencias manual
var presentacionService = new PresentacionService();
var validacionService = new ValidacionService();
var formularioService = new FormularioService(validacionService);
var aplicacionService = new AplicacionService(presentacionService, formularioService);

// Ejecutar la aplicación con la nueva arquitectura
aplicacionService.Ejecutar();


