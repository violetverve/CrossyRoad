using UnityEngine;
using Camera;
using CrossyRoad.Players;

public class CarriedByEagleDeathBehaviour : IDeathBehaviour
{

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        Player.Instance.DeactivatePlayer();
    }

}
