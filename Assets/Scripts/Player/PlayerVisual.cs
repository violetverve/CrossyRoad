using System;
using UnityEngine;

namespace CrossyRoad.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        public static PlayerVisual Instance { get; private set; }

        public static event Action OnHopAnimationComplete;
        private const string HopTrigger = "Hop";
        private Animator animator;

        public Animator Animator => animator;

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            PlayerMovement.OnPlayerMoved += HandleOnPlayerMoved;
        }

        private void OnDisable()
        {
            PlayerMovement.OnPlayerMoved -= HandleOnPlayerMoved;
        }

        private void HandleOnPlayerMoved()
        {
            animator.SetTrigger(HopTrigger);
        }

        public void HopAnimationCompleted()
        {
            OnHopAnimationComplete?.Invoke();
        }

        public void PlayAnimation(string trigger)
        {
            animator.SetTrigger(trigger);
        }

    }
}