namespace Pricing.Core.Rules;

using Pricing.Core.Configuration;

/// <summary>
/// Aplica el IVA y redondea el precio final según la configuración.
/// </summary>
public sealed class TaxRule : IPricingRule
{
    private readonly PricingConfiguration _config;
    
    public TaxRule(PricingConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public int Priority => 900; // Después de descuentos

    public PricingContext Apply(PricingContext context)
    {
        var finalPrice = context.DiscountedAmount * (1 + _config.TaxRate);
        var roundedPrice = Math.Round(
            finalPrice, 
            _config.RoundingDecimals, 
            _config.RoundingMode
        );
        
        return context.WithFinalPrice(roundedPrice);
    }
}
