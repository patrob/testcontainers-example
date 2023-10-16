using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Core.Entities;

public class Post : IEntity, IDateTrackable
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Modified { get; set; }
    
    public string Title { get; set; }
    public string Body { get; set; }
    
    public virtual User User { get; set; }
}