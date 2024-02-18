using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour {

    public static TimeManager Instance { get; private set; }

    public event EventHandler TimeWithoutMovingIsUp;
    [SerializeField] private float maxTimeWithoutMoving = 10;
    private float timeWithoutMoving = 0;
    private bool update = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;

        CrossyGameManager.Instance.OnGameStateChanged += CrossyGameManager_OnGameStateChanged;
    }

    private void CrossyGameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        update = CrossyGameManager.Instance.IsPlaying();
    }

    private void Player_OnPlayerMoved(object sender, System.EventArgs e) {
        timeWithoutMoving = 0;
    }

    private void Update() {
        if (!update) {
            return;
        }

        timeWithoutMoving += Time.deltaTime;

        if (timeWithoutMoving > maxTimeWithoutMoving) {
            update = false;
            TimeWithoutMovingIsUp?.Invoke(this, EventArgs.Empty);
        }
    }

}