using dciSphere.Interaction.Banks;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace dciSphere.Endpoints;

public static class BankEndpoints
{
    public static RouteGroupBuilder GetBankEndpoints(this RouteGroupBuilder api)
    {
        const string tag = "products";

        api.MapPost($"", async ([FromServices] ISender sender, [FromBody] CreateBank.Command command) => await sender.Send(command))
            .WithTags(tag)
            .WithMetadata(new SwaggerOperationAttribute(description: "Create new bank"))
            .AllowAnonymous();

        api.MapGet("{id:int}", async ([FromServices] ISender sender, [FromRoute] int id) => await sender.Send(new GetBankById.Query {Id=id}));

        api.MapGet("", async ([FromServices] ISender sender) => await sender.Send(new GetAllBanks.Query()));
        api.MapGet("browse", async ([FromServices] ISender sender, int page, int size ) => await sender.Send(new BrowseBanks.Query { Page=page, Size=size}));

        return api;
    }
}
