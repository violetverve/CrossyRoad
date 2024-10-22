using UnityEngine;

namespace CrossyRoad.Players.States
{
    public abstract class PlayerStateBase
    {
        protected Player _player;

        public PlayerStateBase(Player player)
        {
            _player = player;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void OnMove(Vector3 direction) { }
        public virtual void OnHopAnimationComplete() { }
        public virtual void Die(IDeathBehaviour deathBehavior) { }
    }
}