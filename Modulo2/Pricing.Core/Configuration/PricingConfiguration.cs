namespace Pricing.Core.Configuration;

public sealed class PricingConfiguration
{
    public decimal MaxDiscount { get; init; } = 0.50m;
    public decimal TaxRate { get; init; } = 0.21m;
    public int RoundingDecimals { get; init; } = 2;
    public MidpointRounding RoundingMode { get; init; } = MidpointRounding.AwayFromZero;
    
    // Descuentos por tipo de cliente
    public Dictionary<string, decimal> CustomerTypeDiscounts { get; init; } = new(StringComparer.OrdinalIgnoreCase)
    {
        ["VIP"] = 0.20m,
        ["Employee"] = 0.30m
    };
    
    // Umbrales de volumen
    public int VolumeDiscountThreshold { get; init; } = 10;
    public decimal VolumeDiscountRate { get; init; } = 0.05m;
    
    // Black Friday
    public int BlackFridayMonth { get; init; } = 11;
    public decimal BlackFridayDiscount { get; init; } = 0.10m;
}
