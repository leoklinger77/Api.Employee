namespace Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public Entity() { }

        public Entity(int id)
        {
            Id = id;
        }
    }
}
