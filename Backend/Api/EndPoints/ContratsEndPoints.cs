using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ContratsEndPoints
{
    public static void MapContratsEndPoints(this WebApplication app)
    {
        app.MapGet("/api/contrats", async (IContratUseCases useCases) =>
        {
            var contrats = await useCases.GetAllAsync();
            return Results.Ok(contrats);
        });

        app.MapGet("/api/contrats/{id}", async (int id, IContratUseCases useCases) =>
        {
            var contrat = await useCases.GetByIdAsync(id);
            return contrat is null ? Results.NotFound() : Results.Ok(contrat);
        });

        app.MapPost("/api/contrats", async (Contrat contrat, IContratUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(contrat);
            return Results.Created($"/api/contrats/{id}", contrat);
        });

        app.MapPut("/api/contrats/{id}", async (int id, Contrat contrat, IContratUseCases useCases) =>
        {
            contrat.IdContrat = id;
            var result = await useCases.UpdateAsync(contrat);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/contrats/{id}", async (int id, IContratUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}