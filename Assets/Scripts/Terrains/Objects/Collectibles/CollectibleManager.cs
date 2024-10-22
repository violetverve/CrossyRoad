using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CrossyRoad.Terrains.Objects.Collectibles
{
    public class CollectibleManager : MonoBehaviour
    {
        [SerializeField] private List<CollectibleBase> _collectibles;
        [SerializeField] private int _maxZPosition = 10;
        [SerializeField] private int _maxAmount = 1;
        [SerializeField] private float _yPosition = 0.5f;
        [SerializeField] private float _spawnProbability = 0.1f; 

        private ObjectManager _objectManager;

        private List<ICollectible> _spawnedCollectibles;

        public void Initialize(ObjectManager objectManager)
        {
            _spawnedCollectibles = new List<ICollectible>();
            _objectManager = objectManager;

            SpawnCollectibleWithProbability();
        }

        private void SpawnCollectibleWithProbability()
        {
            if (UnityEngine.Random.value < _spawnProbability)
            {
                SpawnCollectible(GetSpawnZPosition());
            }
        }

        private int GetSpawnZPosition()
        {
            var takenZPositions = _objectManager.ZPositions.ToList();

            var collectiblePositions = _spawnedCollectibles.Select(c => c.Transform.position.z).ToList();
        
            int zPosition;
            if (_objectManager.WalkableTerrain)
            {
                do 
                {
                    zPosition = UnityEngine.Random.Range(-_maxZPosition, _maxZPosition + 1);
                } while (takenZPositions.Contains(zPosition) || collectiblePositions.Contains(zPosition));
            }
            else
            {
                zPosition  = takenZPositions[UnityEngine.Random.Range(0, takenZPositions.Count)];
            }

            return zPosition;
        }

        public void SpawnCollectible(int zPosition)
        {
            if (_collectibles.Count == 0 || _spawnedCollectibles.Count >= _maxAmount)
            {
                return;
            }

            var collectible = _collectibles[UnityEngine.Random.Range(0, _collectibles.Count)];

            Vector3 position = new Vector3(transform.position.x, _yPosition, zPosition);

            Transform collectibleObject = Instantiate(collectible.Transform, position, Quaternion.identity);
            collectibleObject.parent = transform;

            ICollectible spawnedCollectible = collectibleObject.GetComponent<ICollectible>();

            _spawnedCollectibles.Add(spawnedCollectible);

            spawnedCollectible.OnCollect += HandleCollectibleCollected;
        }
        
        public void RepositionOrSpawnObjects()
        {
            if (_spawnedCollectibles.Count == 0)
            {
                SpawnCollectibleWithProbability();
            }
            else
            {
                RepositionCollectibles();
            }
        }

        private void RepositionCollectibles()
        {
            foreach (var collectible in _spawnedCollectibles)
            {
                var zPosition = GetSpawnZPosition();
                collectible.Transform.position = new Vector3(collectible.Transform.position.x, _yPosition, zPosition);
            }
        }

        private void HandleCollectibleCollected(ICollectible collectible)
        {
            RemoveCollectible(collectible);
        }

        public void RemoveCollectible(ICollectible collectible)
        {
            _spawnedCollectibles.Remove(collectible);
        }
    }
}

