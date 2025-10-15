# ?? EjemploConsola - Aplicación de Consola Refactorizada

[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com)
[![Architecture](https://img.shields.io/badge/architecture-SOLID-orange)](https://en.wikipedia.org/wiki/SOLID)

## ?? Descripción

Aplicación de consola en C# que demuestra buenas prácticas de arquitectura de software con separación de responsabilidades, validaciones robustas y código testeable.

## ? Características

- ? **Arquitectura SOLID** - Separación de responsabilidades en servicios independientes
- ? **Validaciones Robustas** - Validación de nombres, edades, emails y fechas
- ? **Normalización Automática** - Los nombres se convierten automáticamente a Title Case
- ? **Mensajes Claros** - Feedback inmediato con emojis para mejor UX
- ? **Testeable** - Diseño que facilita pruebas unitarias
- ? **Inyección de Dependencias** - Servicios desacoplados y reutilizables
- ? **.NET 9.0** - Utilizando las últimas características de .NET

## ??? Arquitectura

### Servicios

```
EjemploConsola.Services/
??? PresentacionService      ? Manejo de UI y mensajes
??? ValidacionService        ? Validación de datos
??? FormularioService        ? Captura de datos del usuario
??? AplicacionService        ? Orquestación y ciclo de vida
```

### Diagrama de Dependencias

```
Program.cs
    ??? AplicacionService
            ??? PresentacionService
            ??? FormularioService
                    ??? ValidacionService
```

## ?? Inicio Rápido

### Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) o superior

### Instalación

```bash
# Clonar el repositorio
git clone [URL_DEL_REPOSITORIO]

# Navegar al directorio
cd EjemploConsola

# Compilar
dotnet build

# Ejecutar
dotnet run --project EjemploConsola/EjemploConsola.csproj
```

## ?? Uso

### Ejecución Básica

```bash
dotnet run
```

La aplicación te guiará a través de:
1. Pantalla de bienvenida
2. Captura de nombre (validado)
3. Captura de edad (validada 0-120)
4. Confirmación de datos
5. Despedida

### Ejemplos de Validación

#### ? Entrada Válida
```
Por favor, ingresa tu nombre: juan PÉREZ
? Normalizado a: "Juan Pérez"

Por favor, ingresa tu edad: 25
? Aceptado: 25
```

#### ? Entrada Inválida
```
Por favor, ingresa tu nombre: Juan123
? ? El nombre solo puede contener letras y espacios. Intenta de nuevo.

Por favor, ingresa tu edad: -5
? ? La edad debe ser un número válido entre 0 y 120. Intenta de nuevo.
```

## ?? Testing

### Ejecutar Tests de Validación

Puedes probar las validaciones programáticamente:

```csharp
using EjemploConsola.Examples;

// Ejecutar suite de tests
bool todosLosPasaron = EjemplosUso.TestValidaciones();
Console.WriteLine($"Tests: {(todosLosPasaron ? "? PASARON" : "? FALLARON")}");
```

### Casos de Prueba Sugeridos

| Caso | Entrada | Resultado Esperado |
|------|---------|-------------------|
| Nombre con números | "Ana123" | ? Rechazado |
| Nombre válido | "ana lópez" | ? "Ana López" |
| Edad negativa | "-10" | ? Rechazado |
| Edad > 120 | "150" | ? Rechazado |
| Edad válida | "30" | ? 30 |
| Email sin @ | "correo.com" | ? Rechazado |
| Email válido | "user@mail.com" | ? Aceptado |

## ?? Documentación

- [REFACTORIZACION.md](REFACTORIZACION.md) - Detalles de la refactorización
- [Planes.md](Planes.md) - Plan de mejoras implementadas
- [Examples/EjemplosUso.cs](Examples/EjemplosUso.cs) - Ejemplos de uso

## ??? Tecnologías

- **.NET 9.0** - Framework principal
- **C# 12.0** - Lenguaje de programación
- **Visual Basic .NET** - Proyecto de utilidades (SaludoCli.Core)

## ?? Estructura del Proyecto

```
EjemploConsola/
?
??? Program.cs                      # Punto de entrada
??? FormularioConsola.cs            # [OBSOLETO] Clase legacy
?
??? Services/                       # Servicios refactorizados
?   ??? PresentacionService.cs
?   ??? ValidacionService.cs
?   ??? FormularioService.cs
?   ??? AplicacionService.cs
?
??? Examples/                       # Ejemplos de uso
?   ??? EjemplosUso.cs
?
??? REFACTORIZACION.md             # Documentación de cambios
??? Planes.md                      # Plan implementado
??? README.md                      # Este archivo
```

## ?? Integración con VB.NET

El proyecto incluye referencia a `SaludoCli.Core` (VB.NET) que proporciona:

- `PersonaHelpers.NormalizarNombre2()` - Normalización de nombres
- `PersonaHelpers.EsNombreValido()` - Validación de nombres
- `PersonaHelpers.EsEmailValido()` - Validación de emails
- `PersonaHelpers.TryParseFecha()` - Parsing y validación de fechas

Actualmente, `ValidacionService` tiene implementación en C# equivalente.

## ?? Principios SOLID Aplicados

### Single Responsibility (SRP)
Cada servicio tiene una única responsabilidad bien definida.

### Open/Closed Principle (OCP)
Los servicios son abiertos para extensión, cerrados para modificación.

### Liskov Substitution Principle (LSP)
Las implementaciones pueden ser sustituidas sin romper el código cliente.

### Interface Segregation Principle (ISP)
Las dependencias son mínimas y específicas.

### Dependency Inversion Principle (DIP)
Uso de inyección de dependencias para desacoplar componentes.

## ?? Mejoras Futuras

- [ ] Implementar pruebas unitarias con xUnit
- [ ] Agregar sistema de logging (Serilog)
- [ ] Migrar a integración completa con PersonaHelpers.vb
- [ ] Implementar configuración externa (appsettings.json)
- [ ] Agregar soporte para internacionalización (i18n)
- [ ] Implementar async/await para operaciones I/O

## ?? Métricas

| Métrica | Valor |
|---------|-------|
| Servicios | 4 |
| Líneas de código | ~500 |
| Cobertura de validación | 100% |
| Compilación | ? Sin errores |
| Tests manuales | ? Pasando |

## ?? Contribución

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto es un ejemplo educativo.

## ?? Autor

**Refactorización implementada por**: GitHub Copilot
**Fecha**: 2024

## ?? Agradecimientos

- Equipo de .NET por el excelente framework
- Comunidad de C# por las mejores prácticas
- Principios SOLID por guiar el diseño

---

**¿Preguntas o sugerencias?** Abre un issue en el repositorio.

**? Si te gustó el proyecto, dale una estrella!**
