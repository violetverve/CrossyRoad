using UnityEngine;

namespace CrossyRoad.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameOverUI _gameOverUI;
        [SerializeField] private GameStartUI _gameStartUI;

        private void OnEnable()
        {
            CrossyGameManager.OnGameStateChanged += HandleOnGameStateChanged;
        }

        private void OnDisable()
        {
            CrossyGameManager.OnGameStateChanged -= HandleOnGameStateChanged;
        }

        private void HandleOnGameStateChanged()
        {
            switch (CrossyGameManager.Instance.GetGameState())
            {
                case CrossyGameManager.GameState.Playing:
                    _gameStartUI.Hide();
                    break;
                case CrossyGameManager.GameState.Dead:
                    _gameOverUI.Show();
                    break;
            }
        }
    }
}