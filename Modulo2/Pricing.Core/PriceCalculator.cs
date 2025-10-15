namespace Pricing.Core;

public class PriceCalculator
{
    // Calcula el precio final con descuentos y IVA
    public decimal Calculate(decimal unitPrice, int quantity, string? customerType, DateTime purchaseDate)
    {
        var subtotal = unitPrice * quantity;

        // descuentos por tipo de cliente (hardcoded, poco mantenible)
        decimal discount = 0m;
        if (!string.IsNullOrWhiteSpace(customerType))
        {
            if (customerType.Equals("VIP", StringComparison.OrdinalIgnoreCase))
                discount += 0.20m; // 20%
            else if (customerType.Equals("Employee", StringComparison.OrdinalIgnoreCase))
                discount += 0.30m; // 30%
        }

        // descuento por volumen (if anidado)
        if (quantity >= 10)
        {
            discount += 0.05m;
        }

        // descuento “Black Friday” en noviembre (magia de fechas)
        if (purchaseDate.Month == 11)
        {
            discount += 0.10m;
        }

        // límite “silencioso” de descuento total
        if (discount > 0.50m)
        {
            discount = 0.50m;
        }

        var afterDiscounts = subtotal * (1 - discount);

        // IVA fijo 21%
        var final = afterDiscounts * 1.21m;

        // redondeo “a ojo”
        return Math.Round(final, 2, MidpointRounding.AwayFromZero);
    }
}

