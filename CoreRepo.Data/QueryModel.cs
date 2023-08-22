using Microsoft.EntityFrameworkCore.Query;

namespace CoreRepo.Data;

public class QueryModel<T> where T : class
{
    public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; set; }
}