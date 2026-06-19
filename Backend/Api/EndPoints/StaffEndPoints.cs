using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class StaffEndPoints
{
    public static void MapStaffEndPoints(this WebApplication app)
    {
        app.MapGet("/api/staff", async (IStaffUseCases useCases) =>
        {
            var staff = await useCases.GetAllAsync();
            return Results.Ok(staff);
        });

        app.MapGet("/api/staff/{id}", async (int id, IStaffUseCases useCases) =>
        {
            var staff = await useCases.GetByIdAsync(id);
            return staff is null ? Results.NotFound() : Results.Ok(staff);
        });

        app.MapPost("/api/staff", async (Staff staff, IStaffUseCases useCases) =>
        {
            var id = await useCases.CreateAsync(staff);
            return Results.Created($"/api/staff/{id}", staff);
        });

        app.MapPut("/api/staff/{id}", async (int id, Staff staff, IStaffUseCases useCases) =>
        {
            staff.IdStaff = id;
            var result = await useCases.UpdateAsync(staff);
            return result ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/api/staff/{id}", async (int id, IStaffUseCases useCases) =>
        {
            var result = await useCases.DeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        });
    }
}