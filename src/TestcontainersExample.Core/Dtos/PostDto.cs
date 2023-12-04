namespace TestcontainersExample.Core.Dtos;

public class PostDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required UserDto User { get; set; }
}