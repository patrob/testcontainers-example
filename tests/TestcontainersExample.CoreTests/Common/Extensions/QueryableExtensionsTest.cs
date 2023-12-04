using System.Collections;
using FluentAssertions;
using JetBrains.Annotations;
using TestcontainersExample.Core.Common.Extensions;
using TestcontainersExample.Core.Entities;
using Xunit.Categories;

namespace TestcontainersExample.CoreTests.Common.Extensions;

public abstract class BaseTestDataGenerator(List<object[]> data) : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class PageTestDataGenerator() : BaseTestDataGenerator([
    new object[]
    {
        Enumerable.Range(1, 10).ToArray(),
        1,
        5,
        Enumerable.Range(1, 5).ToArray()
    },
    new object[]
    {
        Enumerable.Range(1, 100).ToArray(),
        5,
        20,
        Enumerable.Range(81, 20).ToArray()
    }
]);

public class SortTestDataGenerator() : BaseTestDataGenerator([
    new object[]
    {
        Enumerable.Range(1, 10).Select(n => new Post
        {
            Title = $"Sort By Title {n:00}",
            Body = "some body"
        }).ToArray(),
        "Title",
        true,
        Enumerable.Range(1, 10).Reverse().Select(n => new Post
        {
        Title = $"Sort By Title {n:00}",
        Body = "some body"
        }).ToArray(),
    },
    new object[]
    {
        Enumerable.Range(1, 10).Reverse().Select(n => new Post
        {
            Title = $"Sort Ascending By Title {n:00}",
            Body = "some body"
        }).ToArray(),
        "Title",
        false,
        Enumerable.Range(1, 10).Select(n => new Post
        {
            Title = $"Sort Ascending By Title {n:00}",
            Body = "some body"
        }).ToArray(),
    },
    new object[]
    {
        Enumerable.Range(1, 10).Reverse().Select(n => new Post
        {
            Id = Guid.Parse($"00000000-0000-0000-0000-0000000000{n:00}"),
            Title = $"Sort By Id {n:00}",
            Body = "some body"
        }).ToArray(),
        "Id",
        false,
        Enumerable.Range(1, 10).Select(n => new Post
        {
            Id = Guid.Parse($"00000000-0000-0000-0000-0000000000{n:00}"),
            Title = $"Sort By Id {n:00}",
            Body = "some body"
        }).ToArray()
    }
]);

[TestSubject(typeof(QueryableExtensions))]
public class QueryableExtensionsTest
{
    [Theory]
    [UnitTest]
    [ClassData(typeof(PageTestDataGenerator))]
    public void Page_ShouldPageData(int[] data, int page, int pageSize, int[] expected)
    {
        // Arrange
        var queryable = data.AsQueryable();

        // Act
        var result = queryable.Page(page, pageSize);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [UnitTest]
    [ClassData(typeof(SortTestDataGenerator))]
    public void Sort_ShouldSortData(Post[] data, string columnName, bool isDescending, Post[] expected)
    {
        // Arrange
        var queryable = data.AsQueryable();
        
        // Act
        var result = queryable.Sort(columnName, isDescending);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}