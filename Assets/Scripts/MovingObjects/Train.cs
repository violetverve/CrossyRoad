using UnityEngine;
using CrossyRoad.Players;

namespace CrossyRoad.MovingObjects 
{
    public class Train : MovingObject {

        private void OnCollisionEnter(Collision collision) {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) {
                player.Die(new HitByTrainDeathBehaviour());
            }
        }
    }
}