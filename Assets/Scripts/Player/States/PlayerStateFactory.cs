using UnityEngine;

namespace CrossyRoad.Player.States
{
    public class PlayerStateFactory
    {
        private readonly Player _player;
        private readonly IdleState _idleState;
        private readonly HoppingState _hoppingState;
        private readonly DeadState _deadState;
        private readonly ParalyzedState _paralyzedState;

        private Vector3 _hoppingDirectionPlaceholder = Vector3.zero;

        public PlayerStateFactory(Player player)
        {
            _player = player;
            _idleState = new IdleState(_player);
            _hoppingState = new HoppingState(_player, _hoppingDirectionPlaceholder);
            _paralyzedState = new ParalyzedState(_player);
        }

        
        public PlayerStateBase IdleState => _idleState;

        public PlayerStateBase HoppingState(Vector3 direction)
        {
            _hoppingState.SetDirection(direction);
            return _hoppingState;
        }

        public PlayerStateBase DeadState(IDeathBehaviour deathBehavior)
        {
            return new DeadState(_player, deathBehavior);
        }

        public PlayerStateBase ParalyzedState => _paralyzedState;
    }
}