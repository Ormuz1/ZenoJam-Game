using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightMeter : MonoBehaviour
{
    private float startHeight;
    public static float maxHeightReached = 0;
    private Transform player;
    [SerializeField] private TextMeshProUGUI heightMeterText;
    [SerializeField] private string heightPretext = "Height: ";


    private void OnValidate() {
        if(!(heightMeterText is null))
            heightMeterText.text = GetHeightText();
    }

    private void Awake() {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player") ?? throw new UnassignedReferenceException("There needs to be an object with the player tag.");
        player = playerGO.transform;
        startHeight = player.position.y;
        maxHeightReached = player.position.y - startHeight;
        heightMeterText.text = GetHeightText();
    }

    private void LateUpdate() {
        if((player.position.y - startHeight) > maxHeightReached)
        {
            maxHeightReached = player.position.y - startHeight;
            heightMeterText.text = GetHeightText();
        }
    }


    private string GetHeightText()
    {
        return heightPretext + maxHeightReached.ToString("000.0").Replace(",", "");
    }
}
