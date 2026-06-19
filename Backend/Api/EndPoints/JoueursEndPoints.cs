using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class JoueursEndPoints
{
    public static void MapJoueursEndPoints(this WebApplication app)
    {
        app.MapGet("/api/joueurs", async (IJoueurUseCases useCases) =>
        {
            var joueurs = await useCases.GetAllAsync();
            return Results.Ok(joueurs);
        });

        app.MapGet("/api/joueurs/{id}", async (int id, IJoueurUseCases useCases) =>
        {
            var joueur = await useCases.GetByIdAsync(id);
            return joueur is null ? Results.NotFound() : Results.Ok(joueur);
        });

        app.MapPost("/api/joueurs", async (Joueur joueur, IJoueurUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(joueur);
            return Results.Created($"/api/joueurs/{id}", joueur);
        });

        app.MapPut("/api/joueurs/{id}", async (int id, Joueur joueur, IJoueurUseCases useCases) =>
        {
            joueur.IdJoueur = id;
            var result = await useCases.UpdateAsync(joueur);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/joueurs/{id}", async (int id, IJoueurUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}