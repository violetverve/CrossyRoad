using UnityEngine;
using Camera;
using CrossyRoad.Player;

public class HitByTrainDeathBehaviour : IDeathBehaviour
{

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        Player.Instance.DeactivatePlayer();
    }
}