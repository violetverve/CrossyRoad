using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownDeathBehaviour : IDeathBehaviour {
    private static string DROWN_TRIGGER = "Drown";

    public void Execute() {
        PlayerVisual.Instance.PlayAnimation(DROWN_TRIGGER);

        Vector3 position = Player.Instance.transform.position;
        ParticleManager.Instance.PlayWaterSplash(position);

        Player.Instance.SetParent(null);

        Player.Instance.DeactivatePlayer();
    }
}