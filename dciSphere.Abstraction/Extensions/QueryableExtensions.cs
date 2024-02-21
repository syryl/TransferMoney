using dciSphere.Abstraction.Wrapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Extensions;
public static class QueryableExtensions
{
    public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, int page, int size)
    {
        size = size switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => size
        };
        if (page < 1)
        {
            page = 1;
        }

        var count = query.Count();
        var items = await(query.Skip((page - 1) * size).Take(size).ToListAsync());
        return new(items, count, page, size);
    }
    public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, IPagedQuery pagedQuery) => 
        await PaginateAsync<T>(query, pagedQuery.Page, pagedQuery.Size);
}
