namespace Pricing.Core.Rules;

using Pricing.Core.Configuration;

/// <summary>
/// Aplica descuentos basados en el tipo de cliente (VIP, Employee, etc.).
/// </summary>
public sealed class CustomerTypeDiscountRule : IPricingRule
{
    private readonly PricingConfiguration _config;
    
    public CustomerTypeDiscountRule(PricingConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public int Priority => 100;

    public PricingContext Apply(PricingContext context)
    {
        if (string.IsNullOrWhiteSpace(context.CustomerType))
            return context;
            
        if (_config.CustomerTypeDiscounts.TryGetValue(context.CustomerType, out var discount))
        {
            return context.WithDiscount(discount);
        }
        
        return context;
    }
}
