using UnityEngine;
using Camera;
using CrossyRoad.Players;

public class HitIntoVehicleDeathBehaviour : IDeathBehaviour
{
    private static string HitIntoVehicleTrigger = "HitIntoVehicle";
    private Transform _vehicleTransform;

    public HitIntoVehicleDeathBehaviour(Transform vehicleTransform)
    {
        this._vehicleTransform = vehicleTransform;
    }

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        FollowTarget.StopFollowingTarget?.Invoke();

        PlayerVisual.Instance.PlayAnimation(HitIntoVehicleTrigger);

        Player player = Player.Instance;

        player.SetKinematic(true);
        player.DisableCollider();

        player.transform.SetParent(_vehicleTransform);
    }
}