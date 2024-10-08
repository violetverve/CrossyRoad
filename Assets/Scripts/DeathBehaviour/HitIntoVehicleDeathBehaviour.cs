using UnityEngine;
using Camera;

public class HitIntoVehicleDeathBehaviour : IDeathBehaviour
{
    private static string HIT_INTO_VEHICLE_TRIGGER = "HitIntoVehicle";
    private Transform _vehicleTransform;

    public HitIntoVehicleDeathBehaviour(Transform vehicleTransform)
    {
        this._vehicleTransform = vehicleTransform;
    }

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        FollowTarget.StopFollowingTarget?.Invoke();

        PlayerVisual.Instance.PlayAnimation(HIT_INTO_VEHICLE_TRIGGER);

        Player player = Player.Instance;

        player.SetKinematic(true);
        player.DisableCollider();

        player.transform.SetParent(_vehicleTransform);
    }
}