using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HammerTextDisplay : MonoBehaviour
{
    TextMeshProUGUI textDisplay;
    [SerializeField] CircleHandler circleHandler;

    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        textDisplay.text = circleHandler.currentCircle.name;
    }
}
