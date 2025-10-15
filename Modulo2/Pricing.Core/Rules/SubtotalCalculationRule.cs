namespace Pricing.Core.Rules;

/// <summary>
/// Regla inicial que calcula el subtotal y valida entradas.
/// </summary>
public sealed class SubtotalCalculationRule : IPricingRule
{
    public int Priority => 10; // Primera regla

    public PricingContext Apply(PricingContext context)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(context.UnitPrice);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(context.Quantity);
        
        var subtotal = context.UnitPrice * context.Quantity;
        return context with { Subtotal = subtotal };
    }
}
