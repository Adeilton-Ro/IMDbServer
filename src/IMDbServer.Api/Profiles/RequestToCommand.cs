using AutoMapper;
using IMDb.Application.Features.Account.Adms.Disable;
using IMDb.Application.Features.Account.Adms.Edit;
using IMDb.Application.Features.Account.Clients.Disable;
using IMDb.Application.Features.Account.Clients.Edit;
using IMDbServer.Api.Endpoints.Adm.CustomizedRequests.Disable;
using IMDbServer.Api.Endpoints.Adm.CustomizedRequests.Edit;
using IMDbServer.Api.Endpoints.User.CustomizedRequest.Disable;
using IMDbServer.Api.Endpoints.User.CustomizedRequest.Edit;

namespace IMDbServer.Api.Profiles;
public class RequestToCommand : Profile
{
    public RequestToCommand()
    {
        CreateMap<EditAdmRequest, EditAdmCommand>();
        CreateMap<DisableAdmAccountRequest, DisableAdmAccountCommand>();
        CreateMap<EditClientRequest, EditClientCommand>();
        CreateMap<DisableClientAccountRequest, DisableClientAccountCommand>();
    }
}