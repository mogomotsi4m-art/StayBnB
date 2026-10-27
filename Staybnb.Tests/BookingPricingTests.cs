using Staybnb.Services;
using Xunit;

namespace Staybnb.Tests;

public class BookingPricingTests
{
    [Fact]
    public void Calculate_UsesNightCountAndAddsFeesOnce()
    {
        var result = BookingPricing.Calculate(
            new DateTime(2026, 11, 1), new DateTime(2026, 11, 4),
            pricePerNight: 1200m, cleaningFee: 250m, serviceFee: 75m);

        Assert.Equal(3, result.Nights);
        Assert.Equal(3600m, result.AccommodationSubtotal);
        Assert.Equal(250m, result.CleaningFee);
        Assert.Equal(75m, result.ServiceFee);
        Assert.Equal(3925m, result.TotalPrice);
    }

    [Theory]
    [InlineData(2026, 11, 4, 2026, 11, 4)]
    [InlineData(2026, 11, 5, 2026, 11, 4)]
    public void Calculate_RejectsZeroOrNegativeNightCount(int checkInYear, int checkInMonth, int checkInDay,
        int checkOutYear, int checkOutMonth, int checkOutDay)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BookingPricing.Calculate(
            new DateTime(checkInYear, checkInMonth, checkInDay),
            new DateTime(checkOutYear, checkOutMonth, checkOutDay), 100m, 0m, 0m));
    }

    [Fact]
    public void Calculate_RejectsNegativeFees()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BookingPricing.Calculate(
            new DateTime(2026, 11, 1), new DateTime(2026, 11, 2), 100m, -1m, 0m));
        Assert.Throws<ArgumentOutOfRangeException>(() => BookingPricing.Calculate(
            new DateTime(2026, 11, 1), new DateTime(2026, 11, 2), 100m, 0m, -1m));
    }

    [Fact]
    public void Calculate_RejectsZeroNightlyPrice()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BookingPricing.Calculate(
            new DateTime(2026, 11, 1), new DateTime(2026, 11, 2), 0m, 0m, 0m));
    }
}
