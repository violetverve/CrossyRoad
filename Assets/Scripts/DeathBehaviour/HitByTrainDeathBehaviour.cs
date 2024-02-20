using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByTrainDeathBehaviour : IDeathBehaviour {

    public void Execute() {
        Zoom.Instance.ZoomIn();

        Player.Instance.DeactivatePlayer();
    }
}