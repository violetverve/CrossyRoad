using UnityEngine;

namespace CrossyRoad.Players.States
{
    public class DeadState : PlayerStateBase
    {
        private IDeathBehaviour _deathBehavior;

        public DeadState(Player player, IDeathBehaviour deathBehavior) : base(player)
        {
            _deathBehavior = deathBehavior;
        }

        public override void Enter()
        {
            _deathBehavior.Execute();
            _player.DisableCollider();
            _player.InvokeDeathEvents();
        }
    }
}