using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        SetupTexts();

        InventoryManager.Instance.OnCoinsChanged += InventoryManager_OnCoinsChanged;
        InventoryManager.Instance.OnScoreChanged += InventoryManager_OnScoreChanged;

        Show();
    }


    private void InventoryManager_OnCoinsChanged(object sender, System.EventArgs e) {
        coinText.text = InventoryManager.Instance.GetCoins().ToString();
    }

    private void InventoryManager_OnScoreChanged(object sender, System.EventArgs e) {
        scoreText.text = InventoryManager.Instance.GetScore().ToString();
    }


    private void OnDestroy() {
        InventoryManager.Instance.OnCoinsChanged -= InventoryManager_OnCoinsChanged;
        InventoryManager.Instance.OnScoreChanged -= InventoryManager_OnScoreChanged;
    }



    private void SetupTexts() {
        coinText.text = InventoryManager.Instance.GetCoins().ToString();
        scoreText.text = InventoryManager.Instance.GetScore().ToString();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}