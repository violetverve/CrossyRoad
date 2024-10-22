using System;
using UnityEngine;
using CrossyRoad.Players;

namespace CrossyRoad.Management
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        public event EventHandler TimeWithoutMovingIsUp;
        [SerializeField] private float maxTimeWithoutMoving = 10;
        private float timeWithoutMoving = 0;
        private bool update = false;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            PlayerMovement.OnPlayerMoved += HandleOnPlayerMoved;
            CrossyGameManager.OnGameStateChanged += HandleOnGameStateChanged;
        }

        private void OnDisable()
        {
            PlayerMovement.OnPlayerMoved -= HandleOnPlayerMoved;
            CrossyGameManager.OnGameStateChanged -= HandleOnGameStateChanged;
        }


        private void HandleOnGameStateChanged()
        {
            update = CrossyGameManager.Instance.IsPlaying();
        }

        private void HandleOnPlayerMoved()
        {
            timeWithoutMoving = 0;
        }

        private void Update()
        {
            if (!update)
            {
                return;
            }

            timeWithoutMoving += Time.deltaTime;

            if (timeWithoutMoving > maxTimeWithoutMoving)
            {
                update = false;
                TimeWithoutMovingIsUp?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}