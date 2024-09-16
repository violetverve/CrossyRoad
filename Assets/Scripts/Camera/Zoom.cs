using UnityEngine;
using Cinemachine;
using System;

namespace Camera
{
    public class Zoom : MonoBehaviour
    {
        [SerializeField] private float _zoomedInSize = 4;
        [SerializeField] private float _desiredDuration = 0.8f;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private float _elapsedTime;
        private float _percentageComplete;
        private bool _isZooming;

        public static Action ZoomInStarted;

        private void OnEnable()
        {
            ZoomInStarted += ZoomIn;
        }

        private void OnDisable()
        {
            ZoomInStarted -= ZoomIn;
        }

        private void LateUpdate()
        {
            if (_isZooming)
            {

                _elapsedTime += Time.deltaTime;

                _percentageComplete = _elapsedTime / _desiredDuration;

                _virtualCamera.m_Lens.OrthographicSize =
                    Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, _zoomedInSize, _percentageComplete);

                if (_percentageComplete >= 1)
                {
                    _isZooming = false;
                    _elapsedTime = 0;
                }
            }
        }

        public void ZoomIn()
        {
            _isZooming = true;
        }
    }
}