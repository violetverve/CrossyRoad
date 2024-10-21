using UnityEngine;


namespace CrossyRoad.Player.States
{
    public class IdleState : PlayerStateBase
    {
        private VehicleDetector _vehicleDetector;
        public IdleState(Player player) : base(player)
        {
            _vehicleDetector = new VehicleDetector();
        }
        
        public override void OnMove(Vector3 direction)
        {
            if (!_player.PlayerMovement.CanMove(direction))
            {
                return;
            }

            var movePosition = _player.transform.position + direction;

            if (HandleVehicleCollision(movePosition, direction))
            {
                return;
            }
            
            if (direction != Vector3.zero)
            {
                _player.SetState(_player.StateFactory.HoppingState(direction));
            }
        } 

        private bool HandleVehicleCollision(Vector3 movePosition, Vector3 direction)
        {
            var vehicleTransform = _vehicleDetector.GetVehicleTransform(movePosition);
            _player.PlayerMovement.AdjustPlayerRotation(direction);
            if (vehicleTransform != null)
            {
                HandleDeathState(direction, vehicleTransform);
                return true;
            }
            return false;
        }

        private bool HandleDeathState(Vector3 direction, Transform vehicleTransform)
        {
            IDeathBehaviour deathBehavior = new HitIntoVehicleDeathBehaviour(vehicleTransform);
            if (_player.PlayerMovement.IsMoveToSide(direction))
            {
                deathBehavior = new RunOverDeathBehaviour();
            }

            _player.SetState(_player.StateFactory.DeadState(deathBehavior));
            return true;
        }
    }
}