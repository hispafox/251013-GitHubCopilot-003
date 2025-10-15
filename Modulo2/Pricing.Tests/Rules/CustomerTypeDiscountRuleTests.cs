namespace Pricing.Tests.Rules;

using Pricing.Core;
using Pricing.Core.Configuration;
using Pricing.Core.Rules;

public class CustomerTypeDiscountRuleTests
{
    [Fact]
    public void Apply_VIPCustomer_Adds20PercentDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new CustomerTypeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = "VIP",
            PurchaseDate = DateTime.Now,
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.20m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_EmployeeCustomer_Adds30PercentDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new CustomerTypeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = "Employee",
            PurchaseDate = DateTime.Now,
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.30m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_UnknownCustomerType_NoDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new CustomerTypeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = "Regular",
            PurchaseDate = DateTime.Now,
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_NullCustomerType_NoDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new CustomerTypeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = null,
            PurchaseDate = DateTime.Now,
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0m, result.CurrentDiscount);
    }

    [Fact]
    public void Apply_CaseInsensitiveCustomerType_AppliesDiscount()
    {
        // Arrange
        var config = new PricingConfiguration();
        var rule = new CustomerTypeDiscountRule(config);
        var context = new PricingContext
        {
            UnitPrice = 100m,
            Quantity = 1,
            CustomerType = "vip", // lowercase
            PurchaseDate = DateTime.Now,
            Subtotal = 100m
        };

        // Act
        var result = rule.Apply(context);

        // Assert
        Assert.Equal(0.20m, result.CurrentDiscount);
    }
}
