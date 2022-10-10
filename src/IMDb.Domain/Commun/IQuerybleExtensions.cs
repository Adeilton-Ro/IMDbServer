namespace IMDb.Domain.Commun;
public static class IQuerybleExtensions
{
    public static IEnumerable<TSource> PaginateAndOrder<TSource, TOrderTarget>(this IEnumerable<TSource> queryble, PaginatedQueryOptions paginatedQueryOptions, Func<TSource, TOrderTarget> orderFunction)
    {
        var paginated = queryble.Skip(paginatedQueryOptions.ItensToSkip).Take(paginatedQueryOptions.PageSize);

        return paginatedQueryOptions.IsDescending switch
        {
            true => paginated.OrderByDescending(orderFunction),
            false => paginated.OrderBy(orderFunction),
            _ => paginated
        };
    }
}