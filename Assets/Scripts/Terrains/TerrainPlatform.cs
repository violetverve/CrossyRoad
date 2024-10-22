using UnityEngine;
using CrossyRoad.Terrains.Objects;
using CrossyRoad.Terrains.Objects.Collectibles;

namespace CrossyRoad.Terrains
{
    public class TerrainPlatform : MonoBehaviour
    {
        [SerializeField] private TerrainPlatformType _type;
        [SerializeField] private ObjectManager _objectManager;
        [SerializeField] private CollectibleManager _collectibleManager;

        public ObjectManager ObjectManager => _objectManager;
        public CollectibleManager CollectibleManager => _collectibleManager;
        public TerrainPlatformType Type => _type;

        public void Initialize(TerrainPlatform previousTerrain)
        {
            _objectManager?.Initialize(previousTerrain?.ObjectManager);

            _collectibleManager?.Initialize(_objectManager);
        }

        public void RepositionObjects(TerrainPlatform previousTerrain)
        {
            _objectManager?.RepositionObjects(previousTerrain?.ObjectManager);
            _collectibleManager?.RepositionOrSpawnObjects();
        }
    }


}
