using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class StatistiquesEndPoints
{
    public static void MapStatistiquesEndPoints(this WebApplication app)
    {
        app.MapGet("/api/statistiques/match/{idMatch}", async (int idMatch, IStatistiqueUseCases useCases) =>
        {
            var stats = await useCases.GetByMatchAsync(idMatch);
            return Results.Ok(stats);
        });

        app.MapGet("/api/statistiques/joueur/{idJoueur}", async (int idJoueur, IStatistiqueUseCases useCases) =>
        {
            var stats = await useCases.GetByJoueurAsync(idJoueur);
            return Results.Ok(stats);
        });

        app.MapPost("/api/statistiques", async (Statistique statistique, IStatistiqueUseCases useCases) =>
        {
            var result = await useCases.CreateAsync(statistique);
            return result ? Results.Created("/api/statistiques", statistique) : Results.BadRequest();
        });

        app.MapPut("/api/statistiques", async (Statistique statistique, IStatistiqueUseCases useCases) =>
        {
            var result = await useCases.UpdateAsync(statistique);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/statistiques/{idMatch}/{idJoueur}", async (int idMatch, int idJoueur, IStatistiqueUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(idMatch, idJoueur);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}