namespace Pricing.Core.Rules;

/// <summary>
/// Contrato para reglas de pricing aplicables en orden de prioridad.
/// </summary>
public interface IPricingRule
{
    /// <summary>
    /// Prioridad de ejecuci�n (menor = primero).
    /// 10: C�lculos iniciales
    /// 100: Descuentos base
    /// 200: Descuentos adicionales
    /// 800: Validaciones y l�mites
    /// 900: Aplicaci�n de impuestos
    /// </summary>
    int Priority { get; }
    
    /// <summary>
    /// Aplica la regla al contexto y retorna un nuevo contexto modificado.
    /// </summary>
    PricingContext Apply(PricingContext context);
}
