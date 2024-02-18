using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {
    [SerializeField] private List<Transform> objectPrefabsList;
    [SerializeField] private List<CollactibleObjectSO> collectibleObjectSOList;
    [SerializeField] private int minZPosition = -10;
    [SerializeField] private int maxZPosition = 10;

    [SerializeField] private bool enableDeadZone = false;
    [SerializeField] private int deadZoneZ = 5;

    [SerializeField] private int minObjectCount = 2;
    [SerializeField] private int maxObjectCount = 6;

    [SerializeField] private float yPosition = 0.5f;
    [SerializeField] private bool rotate = false;

    private HashSet<int> zPositions;

    private List<Transform> objectTransformsList;

    private void Awake() {
        zPositions = new HashSet<int>();
        objectTransformsList = new List<Transform>();
    }

    private void Start() {
        int objectCount = Random.Range(minObjectCount, maxObjectCount);

        for (int i = 0; i < objectCount; i++) {
            SpawnObjectOnTerrain();
        }

        SpawnCollectibleWithChance();
    }

    private void SpawnObjectOnTerrain() {
        int zPosition;

        do {
            zPosition = GetRandomZPosition();
        } while (zPositions.Contains(zPosition));


        zPositions.Add(zPosition);

        Vector3 position = new Vector3(transform.position.x, yPosition, zPosition);
        Transform newObject = objectPrefabsList[Random.Range(0, objectPrefabsList.Count)];

        Quaternion rotation = Quaternion.identity;
        if (rotate) {
            int step = 90;
            int randomRotation = Random.Range(0, 4);
            rotation = Quaternion.Euler(0, step * randomRotation, 0);
        }

        Transform terrainObject = Instantiate(newObject, position, rotation);
        terrainObject.parent = transform;

        objectTransformsList.Add(terrainObject);
    }

    private int GetRandomZPosition() {
        int zPosition;

        if (enableDeadZone) {
            bool spawnAboveDeadZone = Random.value > 0.5f;
            if (spawnAboveDeadZone) {
                zPosition = Random.Range(deadZoneZ, maxZPosition);
            } else {
                zPosition = Random.Range(minZPosition, -deadZoneZ);
            }
        } else {
            zPosition = Random.Range(minZPosition, maxZPosition);
        }

        return zPosition;
    }

    private void SpawnCollectibleObject() {
        int zPosition;

        do {
            zPosition = Random.Range(minZPosition, maxZPosition);
        } while (zPositions.Contains(zPosition));

        if (collectibleObjectSOList.Count == 0) {
            return;
        }

        int randomIndex = Random.Range(0, collectibleObjectSOList.Count);

        Transform collectibleObject = collectibleObjectSOList[randomIndex].objectTransform;

        Vector3 position = new Vector3(transform.position.x, yPosition, zPosition);

        Transform terrainObject = Instantiate(collectibleObject, position, Quaternion.identity);
        terrainObject.parent = transform;
    }


    public void RepositionObjects() {
        int newObjectsNum = Random.Range(minObjectCount, maxObjectCount);

        zPositions.Clear();

        int curObjectsNum = objectTransformsList.Count;
        
        int oldObjectsToReposition = Mathf.Min(newObjectsNum, curObjectsNum);
        
        int newObjectsToSpawn = newObjectsNum - oldObjectsToReposition;

        for (int i = 0; i < curObjectsNum; i++) {
            if (i < oldObjectsToReposition) {
                RepositionObeject(objectTransformsList[i]);
            }
        }

        if (curObjectsNum > newObjectsNum) {
            for (int i = curObjectsNum - 1; i >= newObjectsNum; i--) {
                Transform objectToDestroyTransform = objectTransformsList[i];
                objectTransformsList.RemoveAt(i);
                Destroy(objectToDestroyTransform.gameObject);
            }
        }

        if (newObjectsToSpawn > 0) {
            for (int i = 0; i < newObjectsToSpawn; i++) {
                SpawnObjectOnTerrain();
            }
        }

        SpawnCollectibleWithChance();
    }


    private void RepositionObeject(Transform objectTransform) {
        int zPosition;

        do {
            zPosition = GetRandomZPosition();
        } while (zPositions.Contains(zPosition));

        zPositions.Add(zPosition);

        Vector3 position = new Vector3(transform.position.x, yPosition, zPosition);
        objectTransform.position = position;
    }

    private void SpawnCollectibleWithChance() {
        if (Random.Range(0f, 1f) <= 0.3f) {
            SpawnCollectibleObject();
        }
    }

    private void OnDestroy() {
        zPositions.Clear();
        objectTransformsList.Clear();
    }

}