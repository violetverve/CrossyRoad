using UnityEngine;
using System;
using CrossyRoad.Player;

namespace Terrains.Objects.Collectibles
{
    public class Coin : CollectibleBase
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<Player>() != null)
            {
                Collect();
            }
        }

        public override void Collect()
        {
            InventoryManager.Instance.AddCoin();

            base.Collect();
        }
    }
}
