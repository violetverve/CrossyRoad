using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsManager : MonoBehaviour {
    [SerializeField] private Transform railPrefab;

    private float maxZPosition = -10f;
    private float minZPosition = 10f;
    private float zOffset = 2.8f;

    private void Start() {
        float distance = Mathf.Abs(minZPosition - maxZPosition);
        int numberOfRails = Mathf.CeilToInt(distance / zOffset) - 1;

        for (int i = 0; i <= numberOfRails; i++) {
            float zPosition = minZPosition - i * zOffset;
            SpawnRailOnTerrain(zPosition);
        }
    }

    private void SpawnRailOnTerrain(float zPosition) {
        float yPosition = 0.5f;
        Vector3 position = new Vector3(transform.position.x, yPosition, zPosition);
        Transform railObject = Instantiate(railPrefab, position, Quaternion.identity);
        railObject.parent = transform;
    }
}
