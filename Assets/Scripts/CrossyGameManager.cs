using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrossyRoad.Player;

public class CrossyGameManager : MonoBehaviour
{

    public static CrossyGameManager Instance { get; private set; }
    public enum GameState
    {
        Start,
        Playing,
        Dead
    }

    public static event Action OnGameStateChanged;

    private GameState _gameState;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMoved += HandleOnPlayerMoved;
        Player.OnPlayerDied += HandleOnPlayerDied;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMoved -= HandleOnPlayerMoved;
        Player.OnPlayerDied -= HandleOnPlayerDied;
    }

    private void Start()
    {
        SetGameState(GameState.Start);
    }

    private void HandleOnPlayerMoved()
    {
        if (_gameState == GameState.Start)
        {
            SetGameState(GameState.Playing);
        }
    }

    private void HandleOnPlayerDied()
    {
        SetGameState(GameState.Dead);
    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    public bool IsPlaying()
    {
        return _gameState == GameState.Playing;
    }

    public void SetGameState(GameState gameState)
    {
        if (_gameState != gameState)
        {
            _gameState = gameState;
            OnGameStateChanged?.Invoke();
        }
    }

    public void RestartGame()
    {
        InventoryManager.Instance.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}