using UnityEngine;
using CrossyRoad.MovingObjects;

namespace CrossyRoad.Terrains.Rails
{
    public class RailMovingObjectManager : MovingObjectManager
    {
        [SerializeField] private RailLightsVisual _railLightsVisual;

        private const float WarningOffset = 1f;
        private float _warningTime;
        private float _warningTimer;
        private bool _updateWarning;

        private void Start()
        {
            RandomToggleSpawningPoint();
            SetRandomSpawnInteval();

            ResetWarning();
        }

        private void ResetWarning()
        {
            _warningTime = spawnInterval - WarningOffset;
            _warningTimer = 0;
            _updateWarning = true;
        }

        private void Update()
        {
            UpdateTimerAndSpawn();

            if (CheckIfSpawnTimerReset())
            {
                ResetWarning();
            }

            UpdateWarning();
        }

        private void UpdateWarning()
        {
            if (_updateWarning)
            {
                _warningTimer += Time.deltaTime;

                if (_warningTimer >= _warningTime)
                {
                    _railLightsVisual.Warning();
                    _updateWarning = false;
                }
            }
        }
    }
}