using UnityEngine;
using System;
using CrossyRoad.Players;
using CrossyRoad.Management;

namespace CrossyRoad.Terrains.Objects.Collectibles
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
