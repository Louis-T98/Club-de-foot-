using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class EquipesEndPoints
{
    public static void MapEquipesEndPoints(this WebApplication app)
    {
        app.MapGet("/api/equipes", async (IEquipeUseCases useCases) =>
        {
            var equipes = await useCases.GetAllAsync();
            return Results.Ok(equipes);
        });

        app.MapGet("/api/equipes/{id}", async (int id, IEquipeUseCases useCases) =>
        {
            var equipe = await useCases.GetByIdAsync(id);
            return equipe is null ? Results.NotFound() : Results.Ok(equipe);
        });

        app.MapPost("/api/equipes", async (Equipe equipe, IEquipeUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(equipe);
            return Results.Created($"/api/equipes/{id}", equipe);
        });

        app.MapPut("/api/equipes/{id}", async (int id, Equipe equipe, IEquipeUseCases useCases) =>
        {
            equipe.IdEquipe = id;
            var result = await useCases.UpdateAsync(equipe);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/equipes/{id}", async (int id, IEquipeUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}