using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour {

    private void Start() {
        CrossyGameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
    }

    private void Instance_OnGameStateChanged(object sender, System.EventArgs e) {
        if (CrossyGameManager.Instance.GetGameState() == CrossyGameManager.GameState.Start) {
            Show();
        } else {
            Hide();
        }
    }

    private void OnDestroy() {
        CrossyGameManager.Instance.OnGameStateChanged -= Instance_OnGameStateChanged;
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}