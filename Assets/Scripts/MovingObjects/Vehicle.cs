using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MovingObject {

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null) {
            HitIntoVehicleDeathBehaviour deathBehavior = new();
            deathBehavior.SetVehicleTransform(transform);
            Player.Instance.Die(deathBehavior);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null) {
            Player.Instance.Die(new RunOverDeathBehaviour());
        }
    }

}