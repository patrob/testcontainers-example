using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Core.Entities;

public class User : IEntity, IDateTrackable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Modified { get; set; }
}