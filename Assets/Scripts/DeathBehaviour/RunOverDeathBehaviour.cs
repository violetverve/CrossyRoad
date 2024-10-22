using UnityEngine;
using Camera;
using CrossyRoad.Players;

public class RunOverDeathBehaviour : IDeathBehaviour
{
    private static string RunOverTrigger = "RunOver";

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        PlayerVisual.Instance.PlayAnimation(RunOverTrigger);

        Player.Instance.SetKinematic(true);
    }
}