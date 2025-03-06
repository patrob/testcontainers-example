using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Data;
using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Repositories;

namespace TestcontainersExample.IntegrationTests.Repositories;

[Collection("Storage")]
public class AuthorRepositoryTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAuthorRepository _authorRepository;
    private readonly Author? _testAuthor;

    public AuthorRepositoryTests(DatabaseFixture databaseFixture)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(databaseFixture.GetConnection()!)
            .Options;
        _dbContext = new ApplicationDbContext(options);
        _authorRepository = new AuthorRepository(_dbContext);
        
        _testAuthor = new Author{Name = "Bilbo Baggins"};
        databaseFixture.ResetAsync().Wait();
    }
    

    [Fact]
    public void GetAll_ShouldReturnAllAuthors()
    {
        _dbContext.Authors.Add(_testAuthor);
        _dbContext.SaveChanges();
        var authors = _authorRepository.GetAll().ToList();
        authors.Should().HaveCount(1);
        authors.Should().ContainSingle(b => b.Name == _testAuthor.Name);
    }
}