using UnityEngine;
using System;

namespace Camera
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private Vector3 _offset;

        public static Action StopFollowingTarget;

        private void Awake()
        {
            _offset = transform.position - _target.position;
        }

        private void OnEnable()
        {
            StopFollowingTarget += RemoveTarget;
        }

        private void OnDisable()
        {
            StopFollowingTarget -= RemoveTarget;
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            transform.position = _target.position + _offset;
        }

        private void RemoveTarget()
        {
            SetTarget(null);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}