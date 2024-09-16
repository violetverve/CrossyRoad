using UnityEngine;

namespace Collectibles
{
    public interface ICollectible
    {
        Transform Transform { get; }
        void Collect();
    }
}