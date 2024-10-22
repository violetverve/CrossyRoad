using UnityEngine;

namespace CrossyRoad.MovingObjects
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private MovingObjectSO _movingObjectSO;
        private Vector3 _direction;

        public MovingObjectSO MovingObjectSO => _movingObjectSO;
        public Vector3 Direction => _direction;

        protected virtual void Start()
        {
            SetupStartDirection();
        }

        protected virtual void Update()
        {
            UpdatePosition();
        }

        protected void UpdatePosition()
        {
            transform.position += _direction * _movingObjectSO.speed * Time.deltaTime;
        }

        protected void SetupStartDirection()
        {
            if (_direction == Vector3.zero)
            {
                _direction = transform.forward;
            }
        }

        protected void SetDirection(Vector3 newDirection)
        {
            _direction = newDirection;
        }

    }
}

