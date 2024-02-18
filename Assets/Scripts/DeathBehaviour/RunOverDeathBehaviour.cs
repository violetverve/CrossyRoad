using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOverDeathBehaviour : IDeathBehaviour {
    private static string RUN_OVER_TRIGGER = "RunOver";
    
    public void Execute() {
        PlayerVisual.Instance.PlayAnimation(RUN_OVER_TRIGGER);

        Player.Instance.SetKinematic(true);
    }
}