using UnityEngine;
using System;
using CrossyRoad.Player;

public class InventoryManager : MonoBehaviour {

    private const string CoinsKey = "coins";
    public static InventoryManager Instance { get; private set; }
    public event EventHandler OnCoinsChanged;
    public event EventHandler OnScoreChanged;

    private int coins;
    private int score;

    private void Awake() {
        Instance = this;

        coins = LoadCoins();
        score = PlayerPrefs.GetInt("score", 0);
    }

    private void Start()
    {    
        PlayerMovement.OnNewMaxXPositionReached += HandleOnNewMaxXPositionReached;
    }

    private void HandleOnNewMaxXPositionReached() {
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

    public void Save() {
        PlayerPrefs.SetInt(CoinsKey, coins);
    }

    private int LoadCoins() {
        return PlayerPrefs.GetInt(CoinsKey, 0);
    }
}