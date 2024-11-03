using Salestack.Enums;

namespace Salestack.Entities.Customers;

public class PresencialVisit
{
    public Guid Id { get; set; }

    public DateTime VisitDate { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime FinishedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }

    public Guid CustomerId { get; set; }

    public VIsitStatus Status { get; set; } = VIsitStatus.NoStarted;

    public TimeSpan Duration { get; set; }
}
