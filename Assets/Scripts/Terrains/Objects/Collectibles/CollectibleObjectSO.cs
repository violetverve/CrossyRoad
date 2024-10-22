using UnityEngine;

namespace CrossyRoad.Terrains.Objects.Collectibles
{
    [CreateAssetMenu(fileName = "NewCollectibleObject", menuName = "Collectible Object")]
    public class CollectibleObjectSO : ScriptableObject
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;

        public Transform ObjectTransform => _transform;
        public string ObjectName => _name;
        public Sprite ObjectSprite => _sprite;
    }
}