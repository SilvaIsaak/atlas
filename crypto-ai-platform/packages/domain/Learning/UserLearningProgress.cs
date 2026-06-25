using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Learning;

public class UserLearningProgress : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public LearningContent? Content { get; set; }
    public bool IsCompleted { get; set; }
    public int ProgressPercentage { get; set; }
    public DateTime? CompletedAt { get; set; }
}
