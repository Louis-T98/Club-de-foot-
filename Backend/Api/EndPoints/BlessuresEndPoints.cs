using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class BlessuresEndPoints
{
    public static void MapBlessuresEndPoints(this WebApplication app)
    {
        app.MapGet("/api/blessures", async (IBlessureUseCases useCases) =>
        {
            var blessures = await useCases.GetAllAsync();
            return Results.Ok(blessures);
        });

        app.MapGet("/api/blessures/joueur/{idJoueur}", async (int idJoueur, IBlessureUseCases useCases) =>
        {
            var blessures = await useCases.GetByJoueurAsync(idJoueur);
            return Results.Ok(blessures);
        });

        app.MapPost("/api/blessures", async (Blessure blessure, IBlessureUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(blessure);
            return Results.Created($"/api/blessures/{id}", blessure);
        });

        app.MapPut("/api/blessures/{id}", async (int id, Blessure blessure, IBlessureUseCases useCases) =>
        {
            blessure.IdBlessure = id;
            var result = await useCases.UpdateAsync(blessure);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/blessures/{id}", async (int id, IBlessureUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}