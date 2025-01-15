
using ca.Application.CQRS.Peoples.Commands.CreatePeople;
using ca.Application.CQRS.Peoples.Queries.GetPeople;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ca.Web.Endpoints;

public class Peoples : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetPeople,"{id}")
            .MapPost(CreatePeople);
    }

    public Ok<GetPeopleDto> GetPeople(ISender sender,int id)
    {
        GetPeopleQuery query = new();
        query.asignId(id);
        var res = sender.Send(query).Result;

        return TypedResults.Ok(res);
    }

    public Ok<string> GetPeoples(ISender sender)
    {
       
        return TypedResults.Ok("GetPeoples");
    }

    public async Task<Ok<int>> CreatePeople(ISender sender, CreatePeopleCommand command)
    {
        var res = await sender.Send(command);

        return TypedResults.Ok(res);
    }





}
