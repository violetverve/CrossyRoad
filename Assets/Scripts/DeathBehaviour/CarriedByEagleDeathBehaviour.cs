using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedByEagleDeathBehaviour : IDeathBehaviour {

    public void Execute() {
        Zoom.Instance.ZoomIn();

        Player.Instance.DeactivatePlayer();
    }

}
