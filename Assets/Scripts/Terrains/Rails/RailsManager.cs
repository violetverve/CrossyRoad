using UnityEngine;

namespace Terrains.Rails
{
    public class RailsManager : MonoBehaviour
    {
        [SerializeField] private Transform _railPrefab;

        private const float MaxZPosition = -10f;
        private const float MinZPosition = 10f;
        private const float ZOffset = 2.8f;
        private const float YPosition = 0.5f;

        private void Start()
        {
            InitializeRails();
        }

        private void InitializeRails()
        {
            float distance = Mathf.Abs(MinZPosition - MaxZPosition);
            int numberOfRails = Mathf.CeilToInt(distance / ZOffset) - 1;

            for (int i = 0; i <= numberOfRails; i++)
            {
                float zPosition = MinZPosition - i * ZOffset;
                SpawnRailOnTerrain(zPosition);
            }
        }

        private void SpawnRailOnTerrain(float zPosition)
        {
            var position = new Vector3(transform.position.x, YPosition, zPosition);
            Transform railObject = Instantiate(_railPrefab, position, Quaternion.identity);
            railObject.parent = transform;
        }
    }
}