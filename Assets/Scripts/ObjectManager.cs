using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _terrainObjects;
    [SerializeField] private List<Transform> _collectibles;
    [SerializeField] private int _minZPosition = -10;
    [SerializeField] private int _maxZPosition = 10;

    [SerializeField] private bool _enableDeadZone = false;
    [SerializeField] private int _deadZoneZ = 5;

    [SerializeField] private int _minObjectCount = 2;
    [SerializeField] private int _maxObjectCount = 6;

    [SerializeField] private float _yPosition = 0.5f;
    [SerializeField] private bool _rotate = false;

    private HashSet<int> _zPositions;

    private List<Transform> _spawnedObjects;

    private void Awake()
    {
        _zPositions = new HashSet<int>();
        _spawnedObjects = new List<Transform>();
    }

    private void Start()
    {
        int objectCount = Random.Range(_minObjectCount, _maxObjectCount);

        for (int i = 0; i < objectCount; i++)
        {
            SpawnObjectOnTerrain();
        }

        SpawnCollectibleWithChance();
    }

    private void SpawnObjectOnTerrain()
    {
        int zPosition;

        do
        {
            zPosition = GetRandomZPosition();
        } while (_zPositions.Contains(zPosition));


        _zPositions.Add(zPosition);

        Vector3 position = new Vector3(transform.position.x, _yPosition, zPosition);
        Transform newObject = _terrainObjects[Random.Range(0, _terrainObjects.Count)];

        Quaternion rotation = Quaternion.identity;
        if (_rotate)
        {
            int step = 90;
            int randomRotation = Random.Range(0, 4);
            rotation = Quaternion.Euler(0, step * randomRotation, 0);
        }

        Transform terrainObject = Instantiate(newObject, position, rotation);
        terrainObject.parent = transform;

        _spawnedObjects.Add(terrainObject);
    }

    private int GetRandomZPosition()
    {
        int zPosition;

        if (_enableDeadZone)
        {
            bool spawnAboveDeadZone = Random.value > 0.5f;
            if (spawnAboveDeadZone)
            {
                zPosition = Random.Range(_deadZoneZ, _maxZPosition);
            }
            else
            {
                zPosition = Random.Range(_minZPosition, -_deadZoneZ);
            }
        }
        else
        {
            zPosition = Random.Range(_minZPosition, _maxZPosition);
        }

        return zPosition;
    }

    private void SpawnCollectibleObject()
    {
        int zPosition;

        do
        {
            zPosition = Random.Range(_minZPosition, _maxZPosition);
        } while (_zPositions.Contains(zPosition));

        if (_collectibles.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, _collectibles.Count);

        Transform collectibleObject = _collectibles[randomIndex];

        Vector3 position = new Vector3(transform.position.x, _yPosition, zPosition);

        Transform terrainObject = Instantiate(collectibleObject, position, Quaternion.identity);
        terrainObject.parent = transform;
    }


    public void RepositionObjects()
    {
        int newObjectsNum = Random.Range(_minObjectCount, _maxObjectCount);

        _zPositions.Clear();

        int curObjectsNum = _spawnedObjects.Count;

        int oldObjectsToReposition = Mathf.Min(newObjectsNum, curObjectsNum);

        int newObjectsToSpawn = newObjectsNum - oldObjectsToReposition;

        for (int i = 0; i < curObjectsNum; i++)
        {
            if (i < oldObjectsToReposition)
            {
                RepositionObeject(_spawnedObjects[i]);
            }
        }

        if (curObjectsNum > newObjectsNum)
        {
            for (int i = curObjectsNum - 1; i >= newObjectsNum; i--)
            {
                Transform objectToDestroyTransform = _spawnedObjects[i];
                _spawnedObjects.RemoveAt(i);
                Destroy(objectToDestroyTransform.gameObject);
            }
        }

        if (newObjectsToSpawn > 0)
        {
            for (int i = 0; i < newObjectsToSpawn; i++)
            {
                SpawnObjectOnTerrain();
            }
        }

        SpawnCollectibleWithChance();
    }


    private void RepositionObeject(Transform objectTransform)
    {
        int zPosition;

        do
        {
            zPosition = GetRandomZPosition();
        } while (_zPositions.Contains(zPosition));

        _zPositions.Add(zPosition);

        Vector3 position = new Vector3(transform.position.x, _yPosition, zPosition);
        objectTransform.position = position;
    }

    private void SpawnCollectibleWithChance()
    {
        if (Random.Range(0f, 1f) <= 0.3f)
        {
            SpawnCollectibleObject();
        }
    }

    private void OnDestroy()
    {
        _zPositions.Clear();
        _spawnedObjects.Clear();
    }

}