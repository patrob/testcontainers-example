using System.Linq.Expressions;
using System.Reflection;

namespace TestcontainersExample.Core.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
    
    private static Expression<Func<T, TValue>> MemberSelector<T, TValue>(string name)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var body = Expression.PropertyOrField(parameter, name);
        return Expression.Lambda<Func<T, TValue>>(body, parameter);
    }

    private static IQueryable<T> Sort<T,TValue>(this IQueryable<T> query, string columnName, bool isDescending)
    {
        var valueExpression = MemberSelector<T, TValue>(columnName);
        return isDescending ? query.OrderByDescending(valueExpression) : query.OrderBy(valueExpression);
    }

    private static bool IsGuid<T>(string columnName)
    {
        return typeof(T).GetProperty(columnName)!.PropertyType == typeof(Guid);
    }

    private static string GetColumnName<T>(string columnNameIgnoreCase)
    {
        var columnName = typeof(T).GetProperties().Select(p => p.Name)
            .FirstOrDefault(n => n.Equals(columnNameIgnoreCase, StringComparison.OrdinalIgnoreCase));
        return columnName ?? columnNameIgnoreCase;
    }
    
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, string columnName, bool isDescending)
    {
        var sanitizedColumnName = GetColumnName<T>(columnName);
        return IsGuid<T>(sanitizedColumnName) ? query.Sort<T, Guid>(sanitizedColumnName, isDescending) : query.Sort<T, object>(sanitizedColumnName, isDescending);
    }
}