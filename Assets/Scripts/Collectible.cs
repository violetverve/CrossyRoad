using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField] private CollactibleObjectSO collectibleObjectSO;

    private void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<Player>() != null) {
            Collect();
            Destroy(gameObject);
        }
    }

    private void Collect() {
        InventoryManager.Instance.AddCoin();
    }
}