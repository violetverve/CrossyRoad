using System;
using UnityEngine;
using CrossyRoad.Players.States;

namespace CrossyRoad.Players
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        public static event Action OnPlayerDied;

        private BoxCollider _boxCollider;
        private Rigidbody _rigidbody;
        private PlayerMovement _playerMovement;
        private PlayerStateFactory _stateFactory;
        private PlayerStateBase _currentState;

        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerStateFactory StateFactory => _stateFactory;

        private void Awake()
        {
            Instance = this;
            _boxCollider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
            _playerMovement = GetComponent<PlayerMovement>();

            _stateFactory = new PlayerStateFactory(this);

            _currentState = _stateFactory.IdleState;
            _currentState.Enter();
        }

        private void OnEnable()
        {
            PlayerVisual.OnHopAnimationComplete += HandleOnHopAnimationComplete;
            PlayerMovement.OnMovePressed += HandleOnMovePressed;
        }

        private void OnDisable()
        {
            PlayerVisual.OnHopAnimationComplete -= HandleOnHopAnimationComplete;
            PlayerMovement.OnMovePressed -= HandleOnMovePressed;
        }

        private void HandleOnHopAnimationComplete()
        {
            _currentState.OnHopAnimationComplete();
        }

        private void HandleOnMovePressed(Vector3 direction)
        {
            _currentState.OnMove(direction);
        }

        public void InvokeDeathEvents()
        {
            OnPlayerDied?.Invoke();
        }

        public float GetXPosition()
        {
            return transform.position.x;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetKinematic(bool isKinematic)
        {
            _rigidbody.isKinematic = isKinematic;
        }

        public void DisableCollider()
        {
            _boxCollider.enabled = false;
        }

        public void DeactivatePlayer()
        {
            gameObject.SetActive(false);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetState(PlayerStateBase newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Die(IDeathBehaviour deathBehavior)
        {
            SetState(_stateFactory.DeadState(deathBehavior));
        }

        public void Paralyze()
        {
            SetState(_stateFactory.ParalyzedState);
        }
        
    }
}