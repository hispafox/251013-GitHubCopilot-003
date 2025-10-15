namespace Pricing.Core;

using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class PriceCalculator
{
    private readonly IEnumerable<IPricingRule> _rules;
    private readonly PricingConfiguration _config;

    public PriceCalculator(IEnumerable<IPricingRule> rules, PricingConfiguration config)
    {
        _rules = rules?.OrderBy(r => r.Priority) ?? throw new ArgumentNullException(nameof(rules));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    // Constructor con configuración por defecto (para compatibilidad)
    public PriceCalculator() : this(CreateDefaultRules(), new PricingConfiguration())
    {
    }

    public decimal Calculate(decimal unitPrice, int quantity, string? customerType, DateTime purchaseDate)
    {
        var context = new PricingContext
        {
            UnitPrice = unitPrice,
            Quantity = quantity,
            CustomerType = customerType,
            PurchaseDate = purchaseDate
        };

        // Aplica todas las reglas en orden de prioridad
        context = _rules.Aggregate(context, (current, rule) => rule.Apply(current));

        return context.FinalPrice;
    }
    
    private static IEnumerable<IPricingRule> CreateDefaultRules()
    {
        var config = new PricingConfiguration();
        return new IPricingRule[]
        {
            new SubtotalCalculationRule(),
            new CustomerTypeDiscountRule(config),
            new VolumeDiscountRule(config),
            new SeasonalDiscountRule(config),
            new MaxDiscountRule(config),
            new TaxRule(config)
        };
    }
}

