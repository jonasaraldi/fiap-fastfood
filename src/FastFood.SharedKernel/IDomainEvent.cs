namespace FastFood.SharedKernel;

public interface IDomainEvent
{
    int EventVersion { get; set;  }
    DateTime OccurredOn { get; set; }
}