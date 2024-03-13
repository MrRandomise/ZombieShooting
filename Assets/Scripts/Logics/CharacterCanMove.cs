using Atomic;

namespace Logics
{
    public sealed class CharacterCanMove : IEventLogics
    {
        private readonly AtomicVariable<bool> _canMove;
        private readonly AtomicVariable<bool> _isAlive;


        public CharacterCanMove(AtomicVariable<bool> canMove, AtomicVariable<bool> isAlive)
        {
            _canMove = canMove;
            _isAlive = isAlive;
        }

        private void CanMove(bool value)
        {
            _canMove.Value = value;
        }

        public void OnEnable()
        {
            _isAlive.Subscribe(CanMove);
        }

        public void OnDisable()
        {
            _isAlive.UnSubscribe(CanMove);
        }
    }
}
