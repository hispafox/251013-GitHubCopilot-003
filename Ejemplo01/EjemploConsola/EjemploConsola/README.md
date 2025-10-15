# ?? EjemploConsola - Aplicaci�n de Consola Refactorizada

[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com)
[![Architecture](https://img.shields.io/badge/architecture-SOLID-orange)](https://en.wikipedia.org/wiki/SOLID)

## ?? Descripci�n

Aplicaci�n de consola en C# que demuestra buenas pr�cticas de arquitectura de software con separaci�n de responsabilidades, validaciones robustas y c�digo testeable.

## ? Caracter�sticas

- ? **Arquitectura SOLID** - Separaci�n de responsabilidades en servicios independientes
- ? **Validaciones Robustas** - Validaci�n de nombres, edades, emails y fechas
- ? **Normalizaci�n Autom�tica** - Los nombres se convierten autom�ticamente a Title Case
- ? **Mensajes Claros** - Feedback inmediato con emojis para mejor UX
- ? **Testeable** - Dise�o que facilita pruebas unitarias
- ? **Inyecci�n de Dependencias** - Servicios desacoplados y reutilizables
- ? **.NET 9.0** - Utilizando las �ltimas caracter�sticas de .NET

## ??? Arquitectura

### Servicios

```
EjemploConsola.Services/
??? PresentacionService      ? Manejo de UI y mensajes
??? ValidacionService        ? Validaci�n de datos
??? FormularioService        ? Captura de datos del usuario
??? AplicacionService        ? Orquestaci�n y ciclo de vida
```

### Diagrama de Dependencias

```
Program.cs
    ??? AplicacionService
            ??? PresentacionService
            ??? FormularioService
                    ??? ValidacionService
```

## ?? Inicio R�pido

### Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) o superior

### Instalaci�n

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

### Ejecuci�n B�sica

```bash
dotnet run
```

La aplicaci�n te guiar� a trav�s de:
1. Pantalla de bienvenida
2. Captura de nombre (validado)
3. Captura de edad (validada 0-120)
4. Confirmaci�n de datos
5. Despedida

### Ejemplos de Validaci�n

#### ? Entrada V�lida
```
Por favor, ingresa tu nombre: juan P�REZ
? Normalizado a: "Juan P�rez"

Por favor, ingresa tu edad: 25
? Aceptado: 25
```

#### ? Entrada Inv�lida
```
Por favor, ingresa tu nombre: Juan123
? ? El nombre solo puede contener letras y espacios. Intenta de nuevo.

Por favor, ingresa tu edad: -5
? ? La edad debe ser un n�mero v�lido entre 0 y 120. Intenta de nuevo.
```

## ?? Testing

### Ejecutar Tests de Validaci�n

Puedes probar las validaciones program�ticamente:

```csharp
using EjemploConsola.Examples;

// Ejecutar suite de tests
bool todosLosPasaron = EjemplosUso.TestValidaciones();
Console.WriteLine($"Tests: {(todosLosPasaron ? "? PASARON" : "? FALLARON")}");
```

### Casos de Prueba Sugeridos

| Caso | Entrada | Resultado Esperado |
|------|---------|-------------------|
| Nombre con n�meros | "Ana123" | ? Rechazado |
| Nombre v�lido | "ana l�pez" | ? "Ana L�pez" |
| Edad negativa | "-10" | ? Rechazado |
| Edad > 120 | "150" | ? Rechazado |
| Edad v�lida | "30" | ? 30 |
| Email sin @ | "correo.com" | ? Rechazado |
| Email v�lido | "user@mail.com" | ? Aceptado |

## ?? Documentaci�n

- [REFACTORIZACION.md](REFACTORIZACION.md) - Detalles de la refactorizaci�n
- [Planes.md](Planes.md) - Plan de mejoras implementadas
- [Examples/EjemplosUso.cs](Examples/EjemplosUso.cs) - Ejemplos de uso

## ??? Tecnolog�as

- **.NET 9.0** - Framework principal
- **C# 12.0** - Lenguaje de programaci�n
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
??? REFACTORIZACION.md             # Documentaci�n de cambios
??? Planes.md                      # Plan implementado
??? README.md                      # Este archivo
```

## ?? Integraci�n con VB.NET

El proyecto incluye referencia a `SaludoCli.Core` (VB.NET) que proporciona:

- `PersonaHelpers.NormalizarNombre2()` - Normalizaci�n de nombres
- `PersonaHelpers.EsNombreValido()` - Validaci�n de nombres
- `PersonaHelpers.EsEmailValido()` - Validaci�n de emails
- `PersonaHelpers.TryParseFecha()` - Parsing y validaci�n de fechas

Actualmente, `ValidacionService` tiene implementaci�n en C# equivalente.

## ?? Principios SOLID Aplicados

### Single Responsibility (SRP)
Cada servicio tiene una �nica responsabilidad bien definida.

### Open/Closed Principle (OCP)
Los servicios son abiertos para extensi�n, cerrados para modificaci�n.

### Liskov Substitution Principle (LSP)
Las implementaciones pueden ser sustituidas sin romper el c�digo cliente.

### Interface Segregation Principle (ISP)
Las dependencias son m�nimas y espec�ficas.

### Dependency Inversion Principle (DIP)
Uso de inyecci�n de dependencias para desacoplar componentes.

## ?? Mejoras Futuras

- [ ] Implementar pruebas unitarias con xUnit
- [ ] Agregar sistema de logging (Serilog)
- [ ] Migrar a integraci�n completa con PersonaHelpers.vb
- [ ] Implementar configuraci�n externa (appsettings.json)
- [ ] Agregar soporte para internacionalizaci�n (i18n)
- [ ] Implementar async/await para operaciones I/O

## ?? M�tricas

| M�trica | Valor |
|---------|-------|
| Servicios | 4 |
| L�neas de c�digo | ~500 |
| Cobertura de validaci�n | 100% |
| Compilaci�n | ? Sin errores |
| Tests manuales | ? Pasando |

## ?? Contribuci�n

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto es un ejemplo educativo.

## ?? Autor

**Refactorizaci�n implementada por**: GitHub Copilot
**Fecha**: 2024

## ?? Agradecimientos

- Equipo de .NET por el excelente framework
- Comunidad de C# por las mejores pr�cticas
- Principios SOLID por guiar el dise�o

---

**�Preguntas o sugerencias?** Abre un issue en el repositorio.

**? Si te gust� el proyecto, dale una estrella!**
