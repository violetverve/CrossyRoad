using UnityEngine;
using System;

namespace CrossyRoad.Terrains.Objects.Collectibles
{
    public abstract class CollectibleBase : MonoBehaviour, ICollectible
    {
        public event Action<ICollectible> OnCollect;
        public Transform Transform => transform;

        public virtual void Collect()
        {
            OnCollect?.Invoke(this);
            Destroy(gameObject);
        }
    }
}