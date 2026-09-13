using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Seed;

public static class AgentSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.agents.AnyAsync())
            return;

        var agents = new List<Agent>
        {
            new Agent
            {
                Name = "Ahmed Mohamed",
                Phone = "01012345678",
                WhatsApp = "01012345678",
                Email = "ahmed@example.com"
            },

            new Agent
            {
                Name = "Mohamed Ali",
                Phone = "01112345678",
                WhatsApp = "01112345678",
                Email = "mohamed@example.com"
            },

            new Agent
            {
                Name = "Omar Hassan",
                Phone = "01212345678",
                WhatsApp = "01212345678",
                Email = "omar@example.com"
            }
        };

        await context.agents.AddRangeAsync(agents);

        await context.SaveChangesAsync();
    }
}