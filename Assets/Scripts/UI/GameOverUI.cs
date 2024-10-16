using UnityEngine;

namespace CrossyRoad.UI
{
    public class GameOverUI : MonoBehaviour
    {
        private void Awake()
        {
            Hide();
        }
        public void RestartScene()
        {
            CrossyGameManager.Instance.RestartGame();
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}