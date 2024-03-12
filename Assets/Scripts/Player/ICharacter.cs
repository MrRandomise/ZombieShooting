namespace Core
{
    public interface ICharacter
    {
        public Atomic.AtomicEvent<int> OnTakeDamage { get;}
    }
}
