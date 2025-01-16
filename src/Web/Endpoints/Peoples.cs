
using ca.Application.CQRS.Peoples.Commands.CreatePeople;
using ca.Application.CQRS.Peoples.Commands.DeletePeople;
using ca.Application.CQRS.Peoples.Commands.UpdatePeople;
using ca.Application.CQRS.Peoples.Queries.GetListPeople;
using ca.Application.CQRS.Peoples.Queries.GetPeople;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ca.Web.Endpoints;

public class Peoples : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetPeople,"{id}")
            .MapGet(GetPeoples)
            .MapPost(CreatePeople)
            .MapPut(UpdatePeople, "{id}")
            .MapDelete(DeletePeople, "{id}")
            ;
    }

    public static Ok<GetPeopleDto> GetPeople(ISender sender,int id)
    {
        GetPeopleQuery query = new();
        query.asignId(id);
        var res = sender.Send(query).Result;

        return TypedResults.Ok(res);
    }

    public static async Task<Ok<List<GetListPeopleDto>>> GetPeoples(ISender sender, [AsParameters] GetListPeopleQuery query)
    {
        var res = await sender.Send(query);

        return TypedResults.Ok(res);
    }

    public static async Task<Ok<int>> CreatePeople(ISender sender, CreatePeopleCommand command)
    {
        var res = await sender.Send(command);

        return TypedResults.Ok(res);
    }

    public static async Task<IResult> UpdatePeople(ISender sender, UpdatePeopleCommand command, [FromQuery ]int Id)
    {
        if(Id != command.Id) return TypedResults.BadRequest("Id in body and query string must be the same");
        var res = await sender.Send(command);
        return TypedResults.Ok(res);
    }

    public static async Task<Ok<int>> DeletePeople(ISender sender, [FromQuery] int Id)
    {
        DeletePeopleCommand command = new();
        command.Id = Id;
        var res = await sender.Send(command);
        return TypedResults.Ok(res);
    }



}
