using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace Person.Routes
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            var routeGroup = app.MapGroup("person");
            routeGroup.MapPost("", 
                async (PersonRequest req, PersonContext context) =>
            {
                var person = new Persons(req.name);
                await context.AddAsync(person);  // insert
                await context.SaveChangesAsync(); // commit no banco
            });

            routeGroup.MapGet("", 
                async (PersonContext context) =>
            {
                var peoples = await context.People.ToListAsync();  // select * from People
                return Results.Ok(peoples);
            });

            routeGroup.MapPut("{id:guid}", 
                async (Guid id,PersonRequest req, PersonContext context) =>
                {
                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
                    if (person is null)
                        return Results.NotFound();

                    person.ChangeName(req.name);   // update
                    await context.SaveChangesAsync();
                    return Results.Ok(person);
                });

            routeGroup.MapDelete("{id:guid}", 
                async (Guid id, PersonContext context) =>
                {
                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
                    if (person is null)
                        return Results.NotFound();

                    person.SetInactive();  // Soft delete
                    await context.SaveChangesAsync();
                    return Results.Ok(person);
                });
        }
    }
}
