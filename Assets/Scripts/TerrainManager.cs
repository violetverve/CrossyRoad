using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    [SerializeField] private Transform grassTerrain;
    [SerializeField] private List<Transform> terrains;

    private List<Transform> spawnedTerrains;
    private int destructionOffset = 8;
    private int terrainsNumber = 20;
    private int startSpawningXPosition = -4;
    private int safeGrassTerrainsZone = 8;
    private int repositionedTerrains = 0;
    private int WithoutGrassTerrainCounter = 0;
    private const int MinGrassSpawnInterval = 3;
    private const int MaxGrassSpawnInterval = 6;
    private int nextGrassTerrain;

    
    private void Start() {
        Player.Instance.OnPlayerXPositionChanged += Player_OnPlayerXPositionChanged;
    
        ResetNextGrassTerrain();

        spawnedTerrains = new List<Transform>();

        for (int i = 0; i < terrainsNumber; i++) {
            Transform newTerrain;
            Vector3 position = new Vector3(i + startSpawningXPosition, 0, 0);
            if (i < safeGrassTerrainsZone) {
                newTerrain = grassTerrain;
            } else {
                newTerrain = GetNewTerrainToSpawn();
            }
            
            SpawnTerrain(newTerrain, position);
        }
    }

    private void ResetNextGrassTerrain() {
        nextGrassTerrain = Random.Range(MinGrassSpawnInterval, MaxGrassSpawnInterval);
    }

    private Transform GetNewTerrainToSpawn() {
        Transform terrainToSpawn;
        WithoutGrassTerrainCounter++;
        if (WithoutGrassTerrainCounter == nextGrassTerrain) {
            WithoutGrassTerrainCounter = 0;
            terrainToSpawn = grassTerrain;
            ResetNextGrassTerrain();
        } else {
            terrainToSpawn = terrains[Random.Range(0, terrains.Count)];
        }
        return terrainToSpawn;
    }

    private void Player_OnPlayerXPositionChanged(object sender, System.EventArgs e) {
        float xDifference = Player.Instance.GetXPosition() - spawnedTerrains[0].position.x;

        if (xDifference < destructionOffset) return;

        if (repositionedTerrains < safeGrassTerrainsZone) {
            repositionedTerrains++;

            SpawnTerrain(GetNewTerrainToSpawn(), GetNextTerrainPosition());
            DestroyTerrainAt(0);
        } else {
        
            RepositionTerrainAt(0);
        }
    }

    private void RepositionTerrainAt(int index) {
        Transform terrainToReposition = spawnedTerrains[index];
        terrainToReposition.position = GetNextTerrainPosition();

        TryRepositionTerrainObjects(terrainToReposition);

        spawnedTerrains.Add(terrainToReposition);
        spawnedTerrains.RemoveAt(index);
    }

    private void TryRepositionTerrainObjects(Transform terrainToReposition) {
        ObjectManager objectManager = terrainToReposition.gameObject.GetComponent<ObjectManager>();
        if (objectManager != null) {
            objectManager.RepositionObjects();
        }
    }

    private void OnDestroy() {
        Player.Instance.OnPlayerXPositionChanged -= Player_OnPlayerXPositionChanged;
    }

    private void DestroyTerrainAt(int index) {
        Transform terrainToDestroy = spawnedTerrains[index];
        spawnedTerrains.RemoveAt(index);
        Destroy(terrainToDestroy.gameObject);
    }

    private Vector3 GetNextTerrainPosition() {
        return new Vector3(spawnedTerrains[spawnedTerrains.Count - 1].position.x + 1, 0, 0);
    }

    private void SpawnTerrain(Transform newTerrain, Vector3 position) {
        Transform terrain = Instantiate(newTerrain, position, Quaternion.identity);
        terrain.parent = transform;
        spawnedTerrains.Add(terrain);
    }
}
