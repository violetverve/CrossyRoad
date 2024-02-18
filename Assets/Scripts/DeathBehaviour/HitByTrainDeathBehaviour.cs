using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByTrainDeathBehaviour : IDeathBehaviour {

    public void Execute() {
        Player.Instance.DeactivatePlayer();
    }
}