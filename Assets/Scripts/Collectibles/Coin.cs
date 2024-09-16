using UnityEngine;

namespace Collectibles
{
    public class Coin : MonoBehaviour, ICollectible
    {
        public Transform Transform => transform;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<Player>() != null)
            {
                Collect();
            }
        }

        public void Collect()
        {
            InventoryManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
