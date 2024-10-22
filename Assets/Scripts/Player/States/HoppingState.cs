using UnityEngine;

namespace CrossyRoad.Players.States
{
    public class HoppingState : PlayerStateBase
    {
        private Vector3 _direction;

        public HoppingState(Player player, Vector3 direction) : base(player)
        {
            _direction = direction;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public override void Enter()
        {
            _player.PlayerMovement.MovePlayer(_direction);
        }

        public override void OnHopAnimationComplete()
        {
            _player.SetState(_player.StateFactory.IdleState);
        }
    }
}