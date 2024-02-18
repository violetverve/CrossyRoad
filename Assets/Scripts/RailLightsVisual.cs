using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailLightsVisual : MonoBehaviour {

    [SerializeField] private List<Transform> railLights;

    public void SetRailLightsActive(bool active) {
        foreach (Transform railLight in railLights) {
            railLight.gameObject.SetActive(active);
        }
    }

    public void SetRailLightActive(int index, bool active) {
        railLights[index].gameObject.SetActive(active);
    }

    public void FlashRailLights() {
        StartCoroutine(FlashRailLightsCoroutine());
    }

    private IEnumerator FlashRailLightsCoroutine() {
        for (int i = 0; i < 3; i++) {
            SetRailLightActive(0, true);
            SetRailLightActive(1, false);
            yield return new WaitForSeconds(0.1f);
            SetRailLightActive(0, false);
            SetRailLightActive(1, true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Warning() {
        StartCoroutine(WarningCoroutine());
    }


    private IEnumerator WarningCoroutine() {
        FlashRailLights();
        SetRailLightsActive(true);
        yield return new WaitForSeconds(3f);
        SetRailLightsActive(false);
    }
}