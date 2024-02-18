using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovingObjectManager : MovingObjectManager {

    [SerializeField] private RailLightsVisual railLightsVisual;

    private float warningOffset = 1f;
    private float warningTime;
    private float warningTimer = 0f;
    private bool updateWarning;

    private void Start() {
        RandomToggleSpawningPoint();
        SetRandomSpawnInteval();

        warningTime = spawnInterval - warningOffset;
        updateWarning = true;
    }


    private void Update() {

        UpdateTimerAndSpawn();

        if (CheckIfSpawnTimerReset()) {
            warningTime = spawnInterval - warningOffset;
            warningTimer = 0;
            updateWarning = true;
        }

        if (updateWarning) {
            warningTimer += Time.deltaTime;

            if (warningTimer >= warningTime) {
                railLightsVisual.Warning();
                updateWarning = false;
            }
        }
    }
}

