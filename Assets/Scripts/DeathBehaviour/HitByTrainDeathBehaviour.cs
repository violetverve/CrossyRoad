using UnityEngine;
using Camera;

public class HitByTrainDeathBehaviour : IDeathBehaviour
{

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        Player.Instance.DeactivatePlayer();
    }
}