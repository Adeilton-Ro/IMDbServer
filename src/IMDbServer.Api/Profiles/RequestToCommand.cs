using AutoMapper;
using IMDb.Application.Features.Account.Adms.Disable;
using IMDb.Application.Features.Account.Adms.Edit;
using IMDb.Application.Features.Account.Clients.Disable;
using IMDb.Application.Features.Account.Clients.Edit;
using IMDb.Application.Features.Films.NewActor;
using IMDb.Application.Features.Films.NewDirector;
using IMDb.Application.Features.Films.NewFilm;
using IMDb.Infra.FileSystem.Abstraction;
using IMDbServer.Api.Endpoints.Adm.CustomizedRequests.Disable;
using IMDbServer.Api.Endpoints.Adm.CustomizedRequests.Edit;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewActor;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewDirector;
using IMDbServer.Api.Endpoints.Film.CustomizerRequests.NewFilm;
using IMDbServer.Api.Endpoints.User.CustomizedRequest.Disable;
using IMDbServer.Api.Endpoints.User.CustomizedRequest.Edit;
using IMDbServer.Api.Extensions;

namespace IMDbServer.Api.Profiles;
public class RequestToCommand : Profile
{
    public RequestToCommand()
    {
        CreateMap<EditAdmRequest, EditAdmCommand>();
        CreateMap<DisableAdmAccountRequest, DisableAdmAccountCommand>();
        CreateMap<EditClientRequest, EditClientCommand>();
        CreateMap<DisableClientAccountRequest, DisableClientAccountCommand>();

        CreateMap<NewDirectorRequest, NewDirectorCommand>()
            .ForCtorParam("image", ndr => ndr.MapFrom(ndr => new FileImage(ndr.Images.ContentType.Replace("image/", "."), ndr.Images.ToBytes())));
        CreateMap<NewActorRequest, NewActorCommand>()
            .ForCtorParam("image", ndr => ndr.MapFrom(ndr => new FileImage(ndr.Images.ContentType.Replace("image/", "."), ndr.Images.ToBytes())));
        CreateMap<NewFilmRequest, NewFilmCommand>()
            .ForCtorParam("images", ndr => ndr.MapFrom(ndr => ndr.Images.Select(i => new FileImage(i.ContentType.Replace("image/", "."), i.ToBytes()))));
    }
}