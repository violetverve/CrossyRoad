using UnityEngine;
using CrossyRoad.Player;

public class WaterTerrain : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null) {
            Player.Instance.Die(new DrownDeathBehaviour());
        }
    }
}
