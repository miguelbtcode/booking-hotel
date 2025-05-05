namespace Domain.Common
{
    public abstract class Entity<TEntityId> where TEntityId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public TEntityId Id { get; protected init; } = default!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastModifiedAt { get; private set; }
        
        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        protected Entity(TEntityId id)
        {
            if (EqualityComparer<TEntityId>.Default.Equals(id, default))
                throw new ArgumentException("Entity Id cannot be default value", nameof(id));
                
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
        
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }
        
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        
        public void SetModifiedDate()
        {
            LastModifiedAt = DateTime.UtcNow;
        }
    }
}