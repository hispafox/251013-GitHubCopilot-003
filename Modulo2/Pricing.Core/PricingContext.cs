namespace Pricing.Core;

/// <summary>
/// Contexto inmutable que fluye a través de las reglas de pricing.
/// Cada regla retorna un nuevo contexto modificado.
/// </summary>
public sealed record PricingContext
{
    public required decimal UnitPrice { get; init; }
    public required int Quantity { get; init; }
    public required string? CustomerType { get; init; }
    public required DateTime PurchaseDate { get; init; }
    
    // Estado calculado
    public decimal Subtotal { get; init; }
    public decimal CurrentDiscount { get; init; }
    public decimal DiscountedAmount { get; init; }
    public decimal FinalPrice { get; init; }
    
    public PricingContext WithDiscount(decimal additionalDiscount)
    {
        return this with 
        { 
            CurrentDiscount = CurrentDiscount + additionalDiscount 
        };
    }
    
    public PricingContext WithFinalPrice(decimal finalPrice)
    {
        return this with { FinalPrice = finalPrice };
    }
    
    public PricingContext WithDiscountedAmount(decimal amount)
    {
        return this with { DiscountedAmount = amount };
    }
}
