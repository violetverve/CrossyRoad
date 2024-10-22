using UnityEngine;
using System;

namespace CrossyRoad.Terrains.Objects.Collectibles
{
    public interface ICollectible
    {
        Transform Transform { get; }
        event Action<ICollectible> OnCollect;
        void Collect();
    }
}