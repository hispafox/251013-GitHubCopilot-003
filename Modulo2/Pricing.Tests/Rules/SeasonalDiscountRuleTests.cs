namespace Pricing.Tests.Rules;

using Pricing.Core;
using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class SeasonalDiscountRuleTests
{
    [Fact]
    public void Apply_NovemberDate_Adds10PercentDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new SeasonalDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = new DateTime(2024, 11, 15),
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.10m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_NonNovemberDate_NoDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new SeasonalDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = new DateTime(2024, 10, 15),
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0m, result.CurrentDiscount);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(12)]
    public void Apply_NonBlackFridayMonths_NoDiscount(int month)
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new SeasonalDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = new DateTime(2024, month, 15),
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0m, result.CurrentDiscount);
    }
}
