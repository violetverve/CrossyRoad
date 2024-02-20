using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIntoVehicleDeathBehaviour : IDeathBehaviour {
    private static string HIT_INTO_VEHICLE_TRIGGER = "HitIntoVehicle";
    private Transform vehicleTransform;

    public void Execute() {
        Zoom.Instance.ZoomIn();

        FollowTarget.Instance.SetTarget(null);

        PlayerVisual.Instance.PlayAnimation(HIT_INTO_VEHICLE_TRIGGER);

        Player player = Player.Instance;

        player.SetKinematic(true);
        player.DisableCollider();

        player.transform.SetParent(vehicleTransform);
    }

    public void SetVehicleTransform(Transform vehicleTransform) {
        this.vehicleTransform = vehicleTransform;
    }
}