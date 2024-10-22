using UnityEngine;
using TMPro;
using CrossyRoad.Management;

namespace CrossyRoad.UI
{
    public class GamePlayUI : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Start()
        {
            SetupTexts();

            InventoryManager.Instance.OnCoinsChanged += InventoryManager_OnCoinsChanged;
            InventoryManager.Instance.OnScoreChanged += InventoryManager_OnScoreChanged;

            Show();
        }

        private void InventoryManager_OnCoinsChanged(object sender, System.EventArgs e)
        {
            _coinText.text = InventoryManager.Instance.GetCoins().ToString();
        }

        private void InventoryManager_OnScoreChanged(object sender, System.EventArgs e)
        {
            _scoreText.text = InventoryManager.Instance.GetScore().ToString();
        }

        private void OnDestroy()
        {
            InventoryManager.Instance.OnCoinsChanged -= InventoryManager_OnCoinsChanged;
            InventoryManager.Instance.OnScoreChanged -= InventoryManager_OnScoreChanged;
        }

        private void SetupTexts()
        {
            _coinText.text = InventoryManager.Instance.GetCoins().ToString();
            _scoreText.text = InventoryManager.Instance.GetScore().ToString();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}