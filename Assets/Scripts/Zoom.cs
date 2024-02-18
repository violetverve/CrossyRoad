using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour {
    public static Zoom Instance { get; private set; }

    [SerializeField] private float zoomedInSize = 4;
    [SerializeField] private float desiredDuration = 0.8f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private float elapsedTime;
    private float percentageComplete;
    private bool isZooming;


    private void Awake() {
        Instance = this;
    }

    private void LateUpdate() {
        if (isZooming) {

            elapsedTime += Time.deltaTime;

            percentageComplete = elapsedTime / desiredDuration;

            virtualCamera.m_Lens.OrthographicSize =
                Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomedInSize, percentageComplete);

            if (percentageComplete >= 1) {
                isZooming = false;
                elapsedTime = 0;
            }
        }
    }

    public void ZoomIn() {
        isZooming = true;
    }
}