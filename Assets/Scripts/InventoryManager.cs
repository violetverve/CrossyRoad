using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager Instance { get; private set; }
    public event EventHandler OnCoinsChanged;
    public event EventHandler OnScoreChanged;

    private int coins;
    private int score;

    private void Awake() {
        Instance = this;

        coins = PlayerPrefs.GetInt("coins", 0);
        score = PlayerPrefs.GetInt("score", 0);
    }

    private void Start() {
        Player.Instance.OnNewMaxXPositionReached += Instance_OnNewMaxXPositionReached;
    }

    private void Instance_OnNewMaxXPositionReached(object sender, System.EventArgs e) {
        AddScore(1);
    }

    public int GetCoins() {
        return coins;
    }

    public int GetScore() {
        return score;
    }

    public void AddCoin() {
        coins++;
        OnCoinsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddScore(int score) {
        this.score += score;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }
}