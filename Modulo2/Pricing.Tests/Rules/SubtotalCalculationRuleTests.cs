namespace Pricing.Tests.Rules;

using Pricing.Core;
using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class SubtotalCalculationRuleTests
{
    [Fact]
    public void Apply_ValidInputs_CalculatesSubtotal()
    {
        // Arrange
        var rule = new SubtotalCalculationRule();
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 5,
            CustomerType = null,
            PurchaseDate = DateTime.Now
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(500m, result.Subtotal);
    }

    [Fact]
    public void Apply_NegativeUnitPrice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var rule = new SubtotalCalculationRule();
        var context = new PricingContext
        {
            UnitPrice = -10m,
            Quantity = 5,
            CustomerType = null,
            PurchaseDate = DateTime.Now
        };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rule.Apply(context));
    }

    [Fact]
    public void Apply_ZeroQuantity_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var rule = new SubtotalCalculationRule();
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 0,
            CustomerType = null,
            PurchaseDate = DateTime.Now
        };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rule.Apply(context));
    }
}
