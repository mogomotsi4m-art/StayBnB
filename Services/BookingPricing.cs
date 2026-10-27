namespace Staybnb.Services;

public readonly record struct BookingPriceBreakdown(
    int Nights,
    decimal PricePerNight,
    decimal AccommodationSubtotal,
    decimal CleaningFee,
    decimal ServiceFee,
    decimal TotalPrice);

public static class BookingPricing
{
    public static BookingPriceBreakdown Calculate(
        DateTime checkInDate,
        DateTime checkOutDate,
        decimal pricePerNight,
        decimal cleaningFee,
        decimal serviceFee)
    {
        var nights = (checkOutDate.Date - checkInDate.Date).Days;
        if (nights <= 0)
            throw new ArgumentOutOfRangeException(nameof(checkOutDate), "Check-out must be after check-in.");
        if (pricePerNight <= 0)
            throw new ArgumentOutOfRangeException(nameof(pricePerNight), "Nightly price must be greater than zero.");
        if (cleaningFee < 0)
            throw new ArgumentOutOfRangeException(nameof(cleaningFee), "Cleaning fee cannot be negative.");
        if (serviceFee < 0)
            throw new ArgumentOutOfRangeException(nameof(serviceFee), "Service fee cannot be negative.");

        var subtotal = pricePerNight * nights;
        return new BookingPriceBreakdown(nights, pricePerNight, subtotal, cleaningFee, serviceFee,
            subtotal + cleaningFee + serviceFee);
    }
}
