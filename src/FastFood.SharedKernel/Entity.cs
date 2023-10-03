namespace FastFood.SharedKernel;

public abstract class Entity
{
    protected Entity(Ulid id) => Id = id;

    protected Entity()
    {
    }
    
    public Ulid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
}