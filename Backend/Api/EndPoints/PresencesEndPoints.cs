using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class PresencesEndPoints
{
    public static void MapPresencesEndPoints(this WebApplication app)
    {
        app.MapGet("/api/presences/{idEntrainement}", async (int idEntrainement, IPresenceUseCases useCases) =>
        {
            var presences = await useCases.GetByEntrainementAsync(idEntrainement);
            return Results.Ok(presences);
        });

        app.MapPost("/api/presences", async (Presence presence, IPresenceUseCases useCases) =>
        {
            var result = await useCases.CreateAsync(presence);
            return result ? Results.Created("/api/presences", presence) : Results.BadRequest();
        });

        app.MapPut("/api/presences", async (Presence presence, IPresenceUseCases useCases) =>
        {
            var result = await useCases.UpdateAsync(presence);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/presences/{idEntrainement}/{idJoueur}", async (int idEntrainement, int idJoueur, IPresenceUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(idEntrainement, idJoueur);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}