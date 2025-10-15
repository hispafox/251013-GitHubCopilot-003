namespace Pricing.Tests.Rules;

using Pricing.Core;
using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class TaxRuleTests
{
    [Fact]
    public void Apply_StandardTaxRate_Adds21PercentAndRounds()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new TaxRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            DiscountedAmount = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(121.00m, result.FinalPrice); // 100 * 1.21
    }

    [Fact]
    public void Apply_WithDiscountedAmount_CalculatesTaxOnDiscountedPrice()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new TaxRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            CurrentDiscount = 0.20m,
            DiscountedAmount = 80m // 100 - 20% = 80
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(96.80m, result.FinalPrice); // 80 * 1.21
    }

    [Fact]
    public void Apply_PriceRequiringRounding_RoundsTo2Decimals()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new TaxRule(config);
        var context = new PricingContext
        {
            UnitPrice = 10m,
            Quantity = 3,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 30m,
            DiscountedAmount = 30m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(36.30m, result.FinalPrice); // 30 * 1.21 = 36.30
    }

    [Fact]
    public void Apply_CustomTaxRate_UsesConfiguredRate()
    {
        // Arrange
        var config = new PricingConfiguration { TaxRate = 0.10m }; // 10% tax
        var rule = new TaxRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            DiscountedAmount = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(110.00m, result.FinalPrice); // 100 * 1.10
    }
}
