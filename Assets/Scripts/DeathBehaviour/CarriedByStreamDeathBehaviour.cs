using UnityEngine;
using Camera;

public class CarriedByStreamDeathBehaviour : IDeathBehaviour
{

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        FollowTarget.StopFollowingTarget?.Invoke();
    }

}
