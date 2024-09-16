using UnityEngine;
using Camera;

public class RunOverDeathBehaviour : IDeathBehaviour
{
    private static string RUN_OVER_TRIGGER = "RunOver";

    public void Execute()
    {
        Zoom.ZoomInStarted?.Invoke();

        PlayerVisual.Instance.PlayAnimation(RUN_OVER_TRIGGER);

        Player.Instance.SetKinematic(true);
    }
}