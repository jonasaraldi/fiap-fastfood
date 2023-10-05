namespace FastFood.SharedKernel;

public abstract class Entity
{
    protected Entity(Ulid id) => Id = id;

    protected Entity()
    {
        Id = Ulid.NewUlid();
        CreatedAt = DateTime.UtcNow;
    }
    
    public Ulid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
}