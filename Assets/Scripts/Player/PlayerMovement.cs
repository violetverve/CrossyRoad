using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CrossyRoad.Players
{
    public class PlayerMovement : MonoBehaviour
    {
        public static class MoveDirection
        {
            private static readonly Dictionary<Vector2, Vector3> DirectionMap = new Dictionary<Vector2, Vector3>
            {
                { Vector2.right, new Vector3(0, 0, -1)},
                { Vector2.left, new Vector3(0, 0, 1) },
                { Vector2.up, new Vector3(1, 0, 0) },
                { Vector2.down, new Vector3(-1, 0, 0) }
            };

            public static Vector3 ConvertToVector3(Vector2 direction)
            {
                if (DirectionMap.TryGetValue(direction, out Vector3 result))
                {
                    return result;
                }
                else
                {
                    Debug.LogWarning("Invalid direction provided.");
                    return Vector3.zero;
                }
            }
        }

        private int _maxXPositionReached = 0;
        private const string ObstacleTag = "Obstacle";
        public static event Action OnNewMaxXPositionReached;
        public static event Action OnPlayerMoved;
        public static event Action OnPlayerXPositionChanged;
        public static event Action<Vector3> OnMovePressed;

        public void OnMove(InputValue value)
        {
            var direction = value.Get<Vector2>();

            if (direction == Vector2.zero)
            {
                return;
            }

            var convertedDirection = MoveDirection.ConvertToVector3(direction);

            OnMovePressed?.Invoke(convertedDirection);
        }

        private Vector3 CalculateAdjustedDirection(Vector3 direction)
        {
            if (direction.z == 0)
            {
                float zDifference = transform.position.z - Mathf.RoundToInt(transform.position.z);
                direction.z = -zDifference;
            }

            return direction;
        }

        private void CheckIfMaxPositionXReached(Vector3 direction)
        {
            if (direction.x > 0 && transform.position.x > _maxXPositionReached)
            {
                _maxXPositionReached = Mathf.RoundToInt(transform.position.x);
                OnNewMaxXPositionReached?.Invoke();
            }
        }

        public void MovePlayer(Vector3 direction)
        {
            direction = CalculateAdjustedDirection(direction);
            CheckIfMaxPositionXReached(direction);
            SetNewPosition(direction);
            AdjustPlayerRotation(direction);
            InvokeMovementEvents(direction);
        }

        private void SetNewPosition(Vector3 direction)
        {
            transform.position += direction;
        }

        public void AdjustPlayerRotation(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public bool CanMove(Vector3 direction)
        {
            float size = 0.4f;
            Vector3 newPosition = transform.position + direction;
            newPosition.y = 0.2f;

            Collider[] hitColliders = Physics.OverlapBox(newPosition, new Vector3(size, size, size));

            return !hitColliders.Any(hitCollider => hitCollider.CompareTag(ObstacleTag));
        }

        public void InvokeMovementEvents(Vector3 direction)
        {
            if (direction.x != 0)
            {
                OnPlayerXPositionChanged?.Invoke();
            }

            OnPlayerMoved?.Invoke();
        }

        public bool IsMoveToSide(Vector3 direction)
        {
            return direction.z != 0;
        }

    }
}