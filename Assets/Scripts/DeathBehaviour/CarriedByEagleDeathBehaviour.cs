using UnityEngine;
using Camera;

public class CarriedByEagleDeathBehaviour : IDeathBehaviour
{

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        Player.Instance.DeactivatePlayer();
    }

}
