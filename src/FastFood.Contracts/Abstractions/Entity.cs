namespace FastFood.Contracts.Abstractions;

public abstract class Entity
{
    protected Entity(Ulid id) => Id = id;

    protected Entity()
    {
        Id = Ulid.NewUlid();
    }
    
    public Ulid Id { get; private set; }
}  