namespace Pricing.Core.Rules;

using Pricing.Core.Configuration;

/// <summary>
/// Aplica descuento por volumen cuando la cantidad supera un umbral.
/// </summary>
public sealed class VolumeDiscountRule : IPricingRule
{
    private readonly PricingConfiguration _config;
    
    public VolumeDiscountRule(PricingConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public int Priority => 200;

    public PricingContext Apply(PricingContext context)
    {
        if (context.Quantity >= _config.VolumeDiscountThreshold)
        {
            return context.WithDiscount(_config.VolumeDiscountRate);
        }
        
        return context;
    }
}
