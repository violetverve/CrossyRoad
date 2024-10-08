using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrains.Rails
{
    public class RailLightsVisual : MonoBehaviour
    {
        [SerializeField] private List<Transform> _railLights;

        private const float FlashInterval = 0.1f;
        private const float WarningDuration = 3f;
        private const int FlashCount = 3;

        public void SetRailLightsActive(bool active)
        {
            foreach (Transform railLight in _railLights)
            {
                railLight.gameObject.SetActive(active);
            }
        }

        public void SetRailLightActive(int index, bool active)
        {
            if (index < 0 || index >= _railLights.Count)
            {
                Debug.LogWarning($"Index {index} is out of range for rail lights.");
                return;
            }

            _railLights[index].gameObject.SetActive(active);
        }

        public void FlashRailLights()
        {
            StartCoroutine(FlashRailLightsCoroutine());
        }

        private void ToggleRailLights(bool firstLightState, bool secondLightState)
        {
            SetRailLightActive(0, firstLightState);
            SetRailLightActive(1, secondLightState);
        }

        private IEnumerator FlashRailLightsCoroutine()
        {
            for (int i = 0; i < FlashCount; i++)
            {
                ToggleRailLights(true, false);
                yield return new WaitForSeconds(FlashInterval);
                ToggleRailLights(false, true);
                yield return new WaitForSeconds(FlashInterval);
            }
        }

        public void Warning()
        {
            StartCoroutine(WarningCoroutine());
        }


        private IEnumerator WarningCoroutine()
        {
            FlashRailLights();
            SetRailLightsActive(true);
            yield return new WaitForSeconds(WarningDuration);
            SetRailLightsActive(false);
        }
    }
}