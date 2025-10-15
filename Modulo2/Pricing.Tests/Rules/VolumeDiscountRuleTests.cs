namespace Pricing.Tests.Rules;

using Pricing.Core;
using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class VolumeDiscountRuleTests
{
    [Fact]
    public void Apply_QuantityAboveThreshold_Adds5PercentDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new VolumeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 10,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 1000m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.05m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_QuantityBelowThreshold_NoDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new VolumeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 9,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 900m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_ExactlyAtThreshold_AppliesDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new VolumeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 10,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 1000m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.05m, result.CurrentDiscount);
    }
}
