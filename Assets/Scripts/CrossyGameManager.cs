using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CrossyGameManager : MonoBehaviour {

    public static CrossyGameManager Instance { get; private set; }
    public enum GameState {
        Start,
        Playing,
        Dead
    }

    public event EventHandler OnGameStateChanged;

    private GameState gameState;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SetGameState(GameState.Start);

        Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;
        Player.Instance.OnPlayerDied += Player_OnPlayerDied;
    }

#pragma warning disable IDE0060 // Remove unused parameter
    private void Player_OnPlayerMoved(object sender, System.EventArgs e) {
        if (gameState == GameState.Start) {
            SetGameState(GameState.Playing);
        }
    }
#pragma warning restore IDE0060 // Remove unused parameter

    private void Player_OnPlayerDied(object sender, System.EventArgs e) {
        SetGameState(GameState.Dead);
    }

    public GameState GetGameState() {
        return gameState;
    }

    public bool IsPlaying() {
        return gameState == GameState.Playing;
    }

    public void SetGameState(GameState gameState) {
        if (this.gameState == gameState) {
            return;
        }
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);

        // Debug.Log("Game state changed to: " + gameState);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restarting game");
    }


}