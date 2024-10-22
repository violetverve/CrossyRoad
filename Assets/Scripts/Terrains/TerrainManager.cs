using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CrossyRoad.Players;

namespace CrossyRoad.Terrains
{
    public class TerrainManager : MonoBehaviour
    {

        [SerializeField] private TerrainPlatform _grassTerrain;
        [SerializeField] private List<TerrainPlatform> _terrains;
        private List<TerrainPlatform> _spawnedTerrains;

        private int _destructionOffset = 8;
        [SerializeField] private int _terrainsNumber = 20;
        private int _startSpawningXPosition = -4;
        private int _safeGrassTerrainsZone = 8;
        private int repositionedTerrains = 0;
        private int _withoutGrassTerrainCounter = 0;
        private const int MinGrassSpawnInterval = 3;
        private const int MaxGrassSpawnInterval = 6;
        private int _nextGrassTerrain;
        private TerrainPlatform _previousObjectManagerTerrain;

        private void OnEnable()
        {
            PlayerMovement.OnPlayerXPositionChanged += HandleOnPlayerXPositionChanged;
        }

        private void OnDisable()
        {
            PlayerMovement.OnPlayerXPositionChanged -= HandleOnPlayerXPositionChanged;
        }

        private void Start()
        {
            ResetNextGrassTerrain();

            _spawnedTerrains = new List<TerrainPlatform>();

            for (int i = 0; i < _terrainsNumber; i++)
            {
                TerrainPlatform newTerrain;
                Vector3 position = new Vector3(i + _startSpawningXPosition, 0, 0);
                if (i < _safeGrassTerrainsZone)
                {
                    newTerrain = _grassTerrain;
                }
                else
                {
                    newTerrain = GetNewTerrainToSpawn();
                }

                SpawnTerrain(newTerrain, position);
            }
        }

        private void ResetNextGrassTerrain()
        {
            _nextGrassTerrain = Random.Range(MinGrassSpawnInterval, MaxGrassSpawnInterval);
        }

        private TerrainPlatform GetNewTerrainToSpawn()
        {
            TerrainPlatform terrainToSpawn;
            _withoutGrassTerrainCounter++;
            if (_withoutGrassTerrainCounter == _nextGrassTerrain)
            {
                _withoutGrassTerrainCounter = 0;
                terrainToSpawn = _grassTerrain;
                ResetNextGrassTerrain();
            }
            else
            {
                terrainToSpawn = _terrains[Random.Range(0, _terrains.Count)];
            }
            return terrainToSpawn;
        }

        private void HandleOnPlayerXPositionChanged()
        {
            float xDifference = Player.Instance.GetXPosition() - _spawnedTerrains[0].transform.position.x;

            if (xDifference < _destructionOffset) return;

            if (repositionedTerrains < _safeGrassTerrainsZone)
            {
                repositionedTerrains++;

                SpawnTerrain(GetNewTerrainToSpawn(), GetNextTerrainPosition());
                DestroyTerrainAt(0);
            }
            else
            {
                RepositionTerrainAt(0);
            }
        }

        private void RepositionTerrainAt(int index)
        {
            var terrain = _spawnedTerrains[index];
            terrain.transform.position = GetNextTerrainPosition();

            terrain.RepositionObjects(_previousObjectManagerTerrain);

            SetIfPreviousTerrainHasObjectManager(terrain);

            _spawnedTerrains.Add(terrain);
            _spawnedTerrains.RemoveAt(index);

        }

        private void DestroyTerrainAt(int index)
        {
            var terrain = _spawnedTerrains[index];
            _spawnedTerrains.RemoveAt(index);
            Destroy(terrain.gameObject);
        }

        private Vector3 GetNextTerrainPosition()
        {
            return new Vector3(_spawnedTerrains[_spawnedTerrains.Count - 1].transform.position.x + 1, 0, 0);
        }

        private void SpawnTerrain(TerrainPlatform newTerrainPrefab, Vector3 position)
        {
            TerrainPlatform terrain = Instantiate(newTerrainPrefab, position, Quaternion.identity);
            terrain.Initialize(_previousObjectManagerTerrain);
            terrain.transform.parent = transform;
            _spawnedTerrains.Add(terrain);

            SetIfPreviousTerrainHasObjectManager(terrain);
        }

        private void SetIfPreviousTerrainHasObjectManager(TerrainPlatform terrain)
        {
            if (terrain.ObjectManager != null)
            {
                _previousObjectManagerTerrain = terrain;
            }
        }
    }

}
