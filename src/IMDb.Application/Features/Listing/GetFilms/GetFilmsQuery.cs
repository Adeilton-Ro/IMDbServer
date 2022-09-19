using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Listing.GetFilms;
public record GetFilmsQuery(bool? IsDescending, int Page, int QuantityOfItems, IEnumerable<Guid?> Directors, 
    string? Name, IEnumerable<Guid?> Gender, IEnumerable<Guid?> Actors) : IRequest<Result<IEnumerable<GetFilmsQueryResponse>>>;