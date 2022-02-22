namespace Data.Entities
{
    public abstract class ModelBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
