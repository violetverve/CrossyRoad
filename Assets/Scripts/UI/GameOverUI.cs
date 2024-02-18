using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Button restartButton;

    private void Start() {
        restartButton.onClick.AddListener(RestartButton_OnClick);

        CrossyGameManager.Instance.OnGameStateChanged += CrossyGameManager_OnGameStateChanged;

        Hide();
    }

    private void CrossyGameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        if (CrossyGameManager.Instance.GetGameState() == CrossyGameManager.GameState.Dead) {
            Show();
        } else {
            Hide();
        }
    }

    private void RestartButton_OnClick() {
        CrossyGameManager.Instance.RestartGame();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
