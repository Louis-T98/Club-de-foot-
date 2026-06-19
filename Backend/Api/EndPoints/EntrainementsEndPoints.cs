using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class EntrainementsEndPoints
{
    public static void MapEntrainementsEndPoints(this WebApplication app)
    {
        app.MapGet("/api/entrainements", async (IEntrainementUseCases useCases) =>
        {
            var entrainements = await useCases.GetAllAsync();
            return Results.Ok(entrainements);
        });

        app.MapGet("/api/entrainements/{id}", async (int id, IEntrainementUseCases useCases) =>
        {
            var entrainement = await useCases.GetByIdAsync(id);
            return entrainement is null ? Results.NotFound() : Results.Ok(entrainement);
        });

        app.MapPost("/api/entrainements", async (Entrainement entrainement, IEntrainementUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(entrainement);
            return Results.Created($"/api/entrainements/{id}", entrainement);
        });

        app.MapPut("/api/entrainements/{id}", async (int id, Entrainement entrainement, IEntrainementUseCases useCases) =>
        {
            entrainement.IdEntrainement = id;
            var result = await useCases.UpdateAsync(entrainement);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/entrainements/{id}", async (int id, IEntrainementUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}