using System;
using System.Threading.Tasks;
using Atomic;

namespace Logics
{
    public sealed class AddAmo
    {
        private readonly AtomicVariable<int> _addTimeout;
        private readonly AtomicVariable<int> _amoAmount;
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicVariable<int> _maxAmoAmount;
        private readonly AtomicEvent _reloadAmo;

        public AddAmo(AtomicVariable<int> addTimeout, AtomicVariable<int> amoAmount, AtomicVariable<bool> isAlive, AtomicEvent reloadAmo, AtomicVariable<int> maxAmoAmount)
        {
            _addTimeout = addTimeout;
            _amoAmount = amoAmount;
            _isAlive = isAlive;
            _reloadAmo = reloadAmo;
            _maxAmoAmount = maxAmoAmount;
        }

        public async void Awake()
        {
            while (_isAlive.Value)
            {
                await Task.Delay(TimeSpan.FromSeconds(_addTimeout.Value));
                if (_amoAmount.Value < _maxAmoAmount.Value)
                {
                    _amoAmount.Value++;
                    _reloadAmo?.Invoke();    
                }
            }
        }
    }
}
