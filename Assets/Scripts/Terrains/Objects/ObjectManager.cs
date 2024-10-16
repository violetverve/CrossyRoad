using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Terrains.Objects
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField] private bool _walkableTerrain = true;
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

        public HashSet<int> ZPositions => _zPositions;
        public bool WalkableTerrain => _walkableTerrain;

        private void Awake()
        {
            _zPositions = new HashSet<int>();
            _spawnedObjects = new List<Transform>();
        }

        public void Initialize(ObjectManager previous)
        {
            var previousWalkableZPositons = previous?.GetWalkableZPositions() ?? new List<int>();
            var previousWalkableZSegments = previous?.GetWalkableZSegments(previousWalkableZPositons) ?? new List<(int start, int end)>();
            var obligatoryWalkablePositions = GetObligatoryWalkableZPositions(previousWalkableZSegments);
            
            int objectsToSpawn = Mathf.Max(Random.Range(_minObjectCount, _maxObjectCount));
            if (!_walkableTerrain)
            {
                objectsToSpawn = Mathf.Max(objectsToSpawn, obligatoryWalkablePositions.Count);

            }

            SpawnNewObjects(objectsToSpawn, obligatoryWalkablePositions);

            // SpawnCollectibleWithChance();
        }

        private int GetUniqueZPosition()
        {
            int zPosition;

            do
            {
                zPosition = GetRandomZPosition();
            } while (_zPositions.Contains(zPosition));

            return zPosition;
        }

        private void SpawnTerrainObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnObjectOnTerrain();
            }
        }

        private void SpawnObjectOnTerrain()
        {
            int zPosition = GetUniqueZPosition();

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
                    zPosition = Random.Range(_deadZoneZ, _maxZPosition + 1);
                }
                else
                {
                    zPosition = Random.Range(_minZPosition, -_deadZoneZ);
                }
            }
            else
            {
                zPosition = Random.Range(_minZPosition, _maxZPosition + 1);
            }

            return zPosition;
        }

        private void SpawnCollectibleObject()
        {
            if (_collectibles.Count == 0)
            {
                return;
            }
            
            int zPosition = GetUniqueZPosition();

            int randomIndex = Random.Range(0, _collectibles.Count);

            Transform collectibleObject = _collectibles[randomIndex];

            Vector3 position = new Vector3(transform.position.x, _yPosition, zPosition);

            Transform terrainObject = Instantiate(collectibleObject, position, Quaternion.identity);
            terrainObject.parent = transform;
        }


        public void RepositionObjects(ObjectManager previous)
        {
            var previousWalkableZPositons = previous?.GetWalkableZPositions() ?? new List<int>();
            var previousWalkableZSegments = previous?.GetWalkableZSegments(previousWalkableZPositons) ?? new List<(int start, int end)>();
            var obligatoryWalkablePositions = GetObligatoryWalkableZPositions(previousWalkableZSegments);

            int newObjectsNum = Mathf.Max(Random.Range(_minObjectCount, _maxObjectCount), obligatoryWalkablePositions.Count);
            _zPositions.Clear();

            int curObjectsNum = _spawnedObjects.Count;
            int oldObjectsToReposition = Mathf.Min(newObjectsNum, curObjectsNum);
            int newObjectsToSpawn = newObjectsNum - oldObjectsToReposition;

            DestroyUnneededObjects(curObjectsNum, newObjectsNum);
            RepositionNeededObjects(oldObjectsToReposition, obligatoryWalkablePositions);
            SpawnNewObjects(newObjectsToSpawn, obligatoryWalkablePositions);
        }

        private void DestroyUnneededObjects(int curObjectsNum, int newObjectsNum)
        {
            for (int i = curObjectsNum - 1; i >= newObjectsNum; i--)
            {
                Transform objectToDestroyTransform = _spawnedObjects[i];
                _spawnedObjects.RemoveAt(i);
                Destroy(objectToDestroyTransform.gameObject);
            }
        }

        private void RepositionNeededObjects(int oldObjectsToReposition, List<int> obligatoryWalkablePositions)
        {
            for (int i = 0; i < oldObjectsToReposition; i++)
            {
                int zPosition = GetRepositionZPosition(obligatoryWalkablePositions);
                RepositionObject(_spawnedObjects[i], zPosition);
            }
        }

        private int GetRepositionZPosition(List<int> obligatoryWalkablePositions)
        {
            int zPosition;
            if (_walkableTerrain)
            {
                do
                {
                    zPosition = GetUniqueZPosition();
                } while (obligatoryWalkablePositions.Contains(zPosition));
            }
            else
            {
                zPosition = obligatoryWalkablePositions.Count > 0 ? obligatoryWalkablePositions[0] : GetRandomNeighbourZPosition();
                if (obligatoryWalkablePositions.Count > 0) obligatoryWalkablePositions.RemoveAt(0);
            }
            return zPosition;
        }

        private void SpawnNewObjects(int newObjectsToSpawn, List<int> obligatoryWalkablePositions)
        {
            for (int i = 0; i < newObjectsToSpawn; i++)
            {
                int zPosition = GetRepositionZPosition(obligatoryWalkablePositions);
                SpawnObjectOnTerrain(zPosition);
            }
        }

        private bool CheckNeighbourZPosition(int zPosition)
        {
            return FitsInSpawnZone(zPosition) && !_zPositions.Contains(zPosition);
        }

        private int GetRandomNeighbourZPosition()
        {
            if (_zPositions.Count == 0)
            {
                return GetUniqueZPosition();
            }

            List<int> zPositionsList = _zPositions.ToList();

            foreach (var zPosition in zPositionsList)
            {
                int right = zPosition + 1;
                int left = zPosition - 1;

                bool checkRightFirst = Random.value > 0.5f;

                int pos1 = checkRightFirst ? right : left;
                int pos2 = checkRightFirst ? left : right;

                if (CheckNeighbourZPosition(pos1))
                {
                    return pos1;
                }
                else if (CheckNeighbourZPosition(pos2))
                {
                    return pos2;
                }
            }
            return GetUniqueZPosition();
        }

        private bool FitsInSpawnZone(int zPosition)
        {
            return zPosition >= _minZPosition && zPosition <= _maxZPosition;
        }

        private void SpawnObjectOnTerrain(int zPosition)
        {
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

        private void RepositionObject(Transform objectTransform, int zPosition)
        {
            _zPositions.Add(zPosition);

            Vector3 position = new Vector3(transform.position.x, _yPosition, zPosition);
            objectTransform.position = position;
        }

        private void RepositionObject(Transform objectTransform)
        {
            int zPosition = GetUniqueZPosition();

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

        public List<int> GetWalkableZPositions()
        {
            var walkablePositions = new List<int>();

            if (!_walkableTerrain)
            {
                foreach (var obj in _spawnedObjects)
                {
                    walkablePositions.Add(Mathf.RoundToInt(obj.position.z));
                }
            }
            else 
            {
                for (int z = _minZPosition; z <= _maxZPosition; z++)
                {
                    if (! _zPositions.Contains(z))
                    {
                        walkablePositions.Add(z);
                    }
                }
            }

            return walkablePositions;
        }

        public List<(int start, int end)> GetWalkableZSegments(List<int> walkableZPositions)
        {
            var sortedPositions = walkableZPositions.OrderBy(z => z).ToList();
            var segments = new List<(int start, int end)>();
            
            if (sortedPositions.Count == 0)
            {
                return segments;
            }

            int segmentStart = sortedPositions[0];
            int segmentEnd = sortedPositions[0];

            for (int i = 1; i < sortedPositions.Count; i++)
            {
                if (sortedPositions[i] == segmentEnd + 1)
                {
                    segmentEnd = sortedPositions[i];
                }
                else
                {
                    segments.Add((segmentStart, segmentEnd));
                    segmentStart = sortedPositions[i];
                    segmentEnd = sortedPositions[i];
                }
            }

            segments.Add((segmentStart, segmentEnd));

            return segments;
        }

        private (int start, int end) GetFittedSegment((int start, int end) segment)
        {
            int start = Mathf.Max(segment.start, _minZPosition);
            int end = Mathf.Min(segment.end, _maxZPosition);

            return (start, end);
        }

        private List<int> GetObligatoryWalkableZPositions(List<(int start, int end)> previousWalkableZSegments)
        {
            var obligatoryPositions = new List<int>();

            foreach (var segment in previousWalkableZSegments)
            {
                // segment cannot be fitted 
                if (segment.end < _minZPosition || segment.start > _maxZPosition)
                {
                    continue;
                }

                var reducedSegment = GetFittedSegment(segment);

                int randomPosition = Random.Range(reducedSegment.start, reducedSegment.end + 1);

                obligatoryPositions.Add(randomPosition);
            }

            return obligatoryPositions;
        }

    }
}