using UnityEngine;
using Camera;
using CrossyRoad.Player;

public class CarriedByEagleDeathBehaviour : IDeathBehaviour
{

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        Player.Instance.DeactivatePlayer();
    }

}
