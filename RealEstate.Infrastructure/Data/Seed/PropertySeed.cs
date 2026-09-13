using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;

namespace RealEstate.Infrastructure.Data.Seed;

public static class PropertySeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.properties.AnyAsync())
            return;

        var properties = new List<Property>
        {
            new Property
            {
                Title = "Modern Apartment in New Cairo",
                Description = "Modern apartment with a great view and excellent finishing.",
                Price = 2500000,
                PropertyType = PropertyType.Apartment,
                TransactionType = TransactionType.Sale,
                Area = 180,
                Bedrooms = 3,
                Bathrooms = 2,
                Floor = 4,
                Furnished = false,
                Address = "New Cairo",
                City = "Cairo",
                Latitude = 30.0444,
                Longitude = 31.2357,
                Status = PropertyStatus.Published,
                IsFeatured = true,
                ViewsCount = 120,
                agentId = 1
            },

            new Property
            {
                Title = "Luxury Villa in Sheikh Zayed",
                Description = "Luxury villa with private garden and modern finishing.",
                Price = 8500000,
                PropertyType = PropertyType.Villa,
                TransactionType = TransactionType.Sale,
                Area = 420,
                Bedrooms = 5,
                Bathrooms = 4,
                Floor = 0,
                Furnished = false,
                Address = "Sheikh Zayed",
                City = "Giza",
                Latitude = 30.0175,
                Longitude = 30.9740,
                Status = PropertyStatus.Published,
                IsFeatured = true,
                ViewsCount = 250,
                agentId = 1
            },

            new Property
            {
                Title = "Furnished Apartment for Rent",
                Description = "Fully furnished apartment suitable for families.",
                Price = 25000,
                PropertyType = PropertyType.Apartment,
                TransactionType = TransactionType.Rent,
                Area = 140,
                Bedrooms = 2,
                Bathrooms = 2,
                Floor = 6,
                Furnished = true,
                Address = "Nasr City",
                City = "Cairo",
                Latitude = 30.0511,
                Longitude = 31.3656,
                Status = PropertyStatus.Published,
                IsFeatured = false,
                ViewsCount = 80,
                agentId = 1
            }
        };

        await context.properties.AddRangeAsync(properties);

        await context.SaveChangesAsync();
    }
}