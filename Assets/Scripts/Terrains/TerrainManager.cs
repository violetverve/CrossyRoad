using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CrossyRoad.Player;

namespace Terrains
{
    public class TerrainManager : MonoBehaviour {

        [SerializeField] private TerrainPlatform _grassTerrain;
        [SerializeField] private List<TerrainPlatform> _terrains;
        private List<TerrainPlatform> _spawnedTerrains;

        private int destructionOffset = 8;
        [SerializeField] private int terrainsNumber = 20;
        private int startSpawningXPosition = -4;
        private int safeGrassTerrainsZone = 8;
        private int repositionedTerrains = 0;
        private int WithoutGrassTerrainCounter = 0;
        private const int MinGrassSpawnInterval = 3;
        private const int MaxGrassSpawnInterval = 6;
        private int nextGrassTerrain;

        private void OnEnable() {
            PlayerMovement.OnPlayerXPositionChanged += HandleOnPlayerXPositionChanged;
        }

        private void OnDisable() {
           PlayerMovement.OnPlayerXPositionChanged -= HandleOnPlayerXPositionChanged;
        }
        
        private void Start() {
            ResetNextGrassTerrain();

            _spawnedTerrains = new List<TerrainPlatform>();

            for (int i = 0; i < terrainsNumber; i++) {
                TerrainPlatform newTerrain;
                Vector3 position = new Vector3(i + startSpawningXPosition, 0, 0);
                if (i < safeGrassTerrainsZone) {
                    newTerrain = _grassTerrain;
                } else {
                    newTerrain = GetNewTerrainToSpawn();
                }
                
                var previousTerrain = i == 0 ? null : _spawnedTerrains[i-1];
                SpawnTerrain(newTerrain, position, previousTerrain);
            
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Q)) {
                RepositionTerrainAt(0);
            }
        }

        private void ResetNextGrassTerrain() {
            nextGrassTerrain = Random.Range(MinGrassSpawnInterval, MaxGrassSpawnInterval);
        }

        private TerrainPlatform GetNewTerrainToSpawn() {
            TerrainPlatform terrainToSpawn;
            WithoutGrassTerrainCounter++;
            if (WithoutGrassTerrainCounter == nextGrassTerrain) {
                WithoutGrassTerrainCounter = 0;
                terrainToSpawn = _grassTerrain;
                ResetNextGrassTerrain();
            } else {
                terrainToSpawn = _terrains[Random.Range(0, _terrains.Count)];
            }
            return terrainToSpawn;
        }

        private void HandleOnPlayerXPositionChanged() {
            float xDifference = Player.Instance.GetXPosition() - _spawnedTerrains[0].transform.position.x;

            if (xDifference < destructionOffset) return;

            if (repositionedTerrains < safeGrassTerrainsZone) {
                repositionedTerrains++;

                SpawnTerrain(GetNewTerrainToSpawn(), GetNextTerrainPosition(), _spawnedTerrains.Last());
                DestroyTerrainAt(0);
            } else {
            
                RepositionTerrainAt(0);
            }
        }

        private void RepositionTerrainAt(int index) {
            var terrain = _spawnedTerrains[index];
            terrain.transform.position = GetNextTerrainPosition();

            var previousTerrain = _spawnedTerrains.Last();

            terrain.RepositionObjects(previousTerrain);

            _spawnedTerrains.Add(terrain);
            _spawnedTerrains.RemoveAt(index);

        }

        private void DestroyTerrainAt(int index) {
            var terrain = _spawnedTerrains[index];
            _spawnedTerrains.RemoveAt(index);
            Destroy(terrain.gameObject);
        }

        private Vector3 GetNextTerrainPosition() {
            return new Vector3(_spawnedTerrains[_spawnedTerrains.Count - 1].transform.position.x + 1, 0, 0);
        }

        private void SpawnTerrain(TerrainPlatform newTerrainPrefab, Vector3 position, TerrainPlatform previiusTerrainPlatform)
        {
            TerrainPlatform terrain = Instantiate(newTerrainPrefab, position, Quaternion.identity);
            terrain.Initialize(previiusTerrainPlatform);        
            terrain.transform.parent = transform;
            _spawnedTerrains.Add(terrain);
        } 
    }

}
