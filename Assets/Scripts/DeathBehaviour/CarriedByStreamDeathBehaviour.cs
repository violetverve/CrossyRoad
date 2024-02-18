using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedByStreamDeathBehaviour : IDeathBehaviour {

    public void Execute() {
        Zoom.Instance.ZoomIn();

        FollowTarget.Instance.SetTarget(null);
    }

}
