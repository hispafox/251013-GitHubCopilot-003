dotnet new sln -n CopilotModulo2
# Ejemplo A (C#)
dotnet new classlib -n Pricing.Core
dotnet new xunit -n Pricing.Tests
dotnet sln CopilotModulo2.sln add Pricing.Core/Pricing.Core.csproj
dotnet sln CopilotModulo2.sln add Pricing.Tests/Pricing.Tests.csproj
dotnet add Pricing.Tests/Pricing.Tests.csproj reference Pricing.Core/Pricing.Core.csproj
dotnet add Pricing.Tests package FluentAssertions
