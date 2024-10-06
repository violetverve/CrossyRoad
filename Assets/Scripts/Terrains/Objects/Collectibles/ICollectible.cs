using UnityEngine;
using System;

namespace Terrains.Objects.Collectibles
{
    public interface ICollectible
    {
        Transform Transform { get; }
        event Action<ICollectible> OnCollect;
        void Collect();
    }
}