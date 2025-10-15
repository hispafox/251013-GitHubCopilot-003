namespace Pricing.Tests.Rules;

using Pricing.Core;
using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class MaxDiscountRuleTests
{
    [Fact]
    public void Apply_DiscountBelowMax_KeepsOriginalDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new MaxDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            CurrentDiscount = 0.30m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.30m, result.CurrentDiscount);
        Assert.Equal(70m, result.DiscountedAmount); // 100 * (1 - 0.30)
    }

    [Fact]
    public void Apply_DiscountAboveMax_CapsAtMaxDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new MaxDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            CurrentDiscount = 0.65m // 65% > 50% max
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.50m, result.CurrentDiscount); // Capped at 50%
        Assert.Equal(50m, result.DiscountedAmount); // 100 * (1 - 0.50)
    }

    [Fact]
    public void Apply_DiscountExactlyAtMax_KeepsMaxDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new MaxDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            CurrentDiscount = 0.50m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.50m, result.CurrentDiscount);
        Assert.Equal(50m, result.DiscountedAmount); // 100 * (1 - 0.50)
    }

    [Fact]
    public void Apply_NoDiscount_KeepsFullSubtotal()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new MaxDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m,
            CurrentDiscount = 0m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0m, result.CurrentDiscount);
        Assert.Equal(100m, result.DiscountedAmount);
    }
}
