using System;
using UnityEngine;

namespace CrossyRoad.Utils
{
    public class SwipeInput : MonoBehaviour
    {
        [Header("Swipe Settings")]
        [SerializeField] private float _sensitivity = 0.02f;

        private Vector3 _startTouchPosition;
        private Vector3 _endTouchPosition;
        private float _minimumSwipeDistance;

        public static event Action<Vector2> SwipeDetected;
        public static event Action TapDetected;

        private void Start()
        {
            _minimumSwipeDistance = Screen.height * _sensitivity;
        }

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                HandleTouch(Input.GetTouch(0));
            }
        }

        private void HandleTouch(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    _endTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    _endTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    _endTouchPosition = touch.position;
                    EvaluateGesture();
                    break;
            }
        }

        private void EvaluateGesture()
        {
            Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;

            if (IsSwipeDetected(swipeDelta))
            {
                OnSwipe(GetSwipeDirection(swipeDelta));
            }
            else
            {
                OnTap();
            }
        }

        private bool IsSwipeDetected(Vector2 swipeDelta)
        {
            return swipeDelta.magnitude > _minimumSwipeDistance;
        }

        private Vector2 GetSwipeDirection(Vector2 swipeDelta)
        {
            if (IsHorizontalSwipe(swipeDelta))
            {
                return _endTouchPosition.x > _startTouchPosition.x ? Vector2.right : Vector2.left;
            }
            else
            {
                return _endTouchPosition.y > _startTouchPosition.y ? Vector2.up : Vector2.down;
            }
        }

        private bool IsHorizontalSwipe(Vector2 swipeDelta)
        {
            return Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y);
        }


        private void OnSwipe(Vector2 direction)
        {
            SwipeDetected?.Invoke(direction);
        }

        private void OnTap()
        {
            TapDetected?.Invoke();
        }
    }
}
