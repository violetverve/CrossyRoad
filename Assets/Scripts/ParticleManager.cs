using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public static ParticleManager Instance { get; private set; }

    [SerializeField] private Transform waterSplashTransform;

    private void Awake() {
        Instance = this;
    }

    public void PlayWaterSplash(Vector3 position) {
        Instantiate(waterSplashTransform, position, Quaternion.identity);
    }

}