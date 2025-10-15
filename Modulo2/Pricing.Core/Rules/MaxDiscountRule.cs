namespace Pricing.Core.Rules;

using Pricing.Core.Configuration;

/// <summary>
/// Aplica el límite máximo de descuento y calcula el monto final con descuentos.
/// </summary>
public sealed class MaxDiscountRule : IPricingRule
{
    private readonly PricingConfiguration _config;
    
    public MaxDiscountRule(PricingConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public int Priority => 800; // Antes de aplicar impuestos

    public PricingContext Apply(PricingContext context)
    {
        var cappedDiscount = Math.Min(context.CurrentDiscount, _config.MaxDiscount);
        var discountedAmount = context.Subtotal * (1 - cappedDiscount);
        
        return context with 
        { 
            CurrentDiscount = cappedDiscount,
            DiscountedAmount = discountedAmount
        };
    }
}
