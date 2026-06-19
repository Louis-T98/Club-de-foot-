using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class MatchsEndPoints
{
    public static void MapMatchsEndPoints(this WebApplication app)
    {
        app.MapGet("/api/matchs", async (IMatchUseCases useCases) =>
        {
            var matchs = await useCases.GetAllAsync();
            return Results.Ok(matchs);
        });

        app.MapGet("/api/matchs/{id}", async (int id, IMatchUseCases useCases) =>
        {
            var match = await useCases.GetByIdAsync(id);
            return match is null ? Results.NotFound() : Results.Ok(match);
        });

        app.MapPost("/api/matchs", async (Match match, IMatchUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(match);
            return Results.Created($"/api/matchs/{id}", match);
        });

        app.MapPut("/api/matchs/{id}", async (int id, Match match, IMatchUseCases useCases) =>
        {
            match.IdMatch = id;
            var result = await useCases.UpdateAsync(match);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/matchs/{id}", async (int id, IMatchUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}