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
            if (vehicleTransform != null)
            {
                _player.PlayerMovement.AdjustPlayerRotation(direction);
                _player.SetState(_player.StateFactory.DeadState(new HitIntoVehicleDeathBehaviour(vehicleTransform)));
                return true;
            }
            return false;
        }
    }
}