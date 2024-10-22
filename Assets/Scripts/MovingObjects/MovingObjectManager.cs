using System.Collections.Generic;
using UnityEngine;

namespace CrossyRoad.MovingObjects
{
    public class MovingObjectManager : MonoBehaviour
    {
        [SerializeField] private List<MovingObjectSO> movingObjectSOList;

        private List<Transform> spawnedMovingObjects;

        [SerializeField] private float spawnedObjectsMax = 5;
        [SerializeField] private float spawnIntervalMax;
        [SerializeField] private float spawnIntervalMin;
        [SerializeField] private bool isRight;

        protected float spawnInterval;
        private float spawnTimer;

        private void Awake()
        {
            spawnedMovingObjects = new List<Transform>();
        }

        private void Start()
        {
            RandomToggleSpawningPoint();

            SetRandomSpawnInteval();
        }

        private void Update()
        {
            UpdateTimerAndSpawn();
        }

        protected void UpdateTimerAndSpawn()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                if (spawnedMovingObjects.Count < spawnedObjectsMax)
                {
                    SpawnMovingObject();
                }
                else
                {
                    ResetFirstMovingObjectPosition();
                }
                ResetSpawnTimer();
            }
        }

        private void ResetFirstMovingObjectPosition()
        {
            Transform firstMovingObject = spawnedMovingObjects[0];
            spawnedMovingObjects.RemoveAt(0);
            firstMovingObject.position = transform.position;
            spawnedMovingObjects.Add(firstMovingObject);
        }

        protected bool CheckIfSpawnTimerReset()
        {
            return spawnTimer == 0;
        }

        protected void ResetSpawnTimer()
        {
            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            spawnTimer = 0;
        }

        private void SpawnMovingObject()
        {
            Quaternion rotation = isRight ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            MovingObjectSO movingObejctSO = movingObjectSOList[Random.Range(0, movingObjectSOList.Count)];
            Transform movingObject = Instantiate(movingObejctSO.objectTransform, transform.position, rotation);
            movingObject.parent = transform;
            spawnedMovingObjects.Add(movingObject);
        }

        protected void RandomToggleSpawningPoint()
        {
            int toggleSpawningPoint = Random.Range(0, 2);
            if (toggleSpawningPoint == 1)
            {
                ToggleSpawningPoint();
            }
        }

        private void ToggleSpawningPoint()
        {
            isRight = !isRight;
            Vector3 newLocalPosition = transform.localPosition;
            newLocalPosition.z = -newLocalPosition.z;
            transform.localPosition = newLocalPosition;
        }

        protected void SetRandomSpawnInteval()
        {
            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }

    }
}