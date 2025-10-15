namespace Pricing.Core.Rules;

using Pricing.Core.Configuration;

/// <summary>
/// Aplica descuentos estacionales como Black Friday (mes de noviembre).
/// </summary>
public sealed class SeasonalDiscountRule : IPricingRule
{
    private readonly PricingConfiguration _config;
    
    public SeasonalDiscountRule(PricingConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public int Priority => 300;

    public PricingContext Apply(PricingContext context)
    {
        // Black Friday: todo el mes de noviembre
        if (context.PurchaseDate.Month == _config.BlackFridayMonth)
        {
            return context.WithDiscount(_config.BlackFridayDiscount);
        }
        
        return context;
    }
}
