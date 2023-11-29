namespace TestcontainersExample.Core.Common.Models;

public class PagedResult<T>
{
    public required List<T> Items { get; set; }
    public int TotalCount { get; set; }
}