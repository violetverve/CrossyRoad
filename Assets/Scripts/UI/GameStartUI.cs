using UnityEngine;

namespace CrossyRoad.UI
{
    public class GameStartUI : MonoBehaviour
    {
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