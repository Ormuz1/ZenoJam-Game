using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HeightText : MonoBehaviour
{
    TextMeshProUGUI textComponent;

    private void Start() {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = "You reached a height of " + HeightMeter.maxHeightReached.ToString("000.0").Replace(",", "") + " meters!";
    }
}
