namespace TestcontainersExample.Core.Common.Interfaces;

public interface IDateTrackable
{
    DateTimeOffset Created { get; set; }
    DateTimeOffset Modified { get; set; }
}