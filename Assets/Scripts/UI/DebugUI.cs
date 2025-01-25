using System;
using CaptainNemo.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public PressureControl pressureControl;
    
    public TextMeshProUGUI textComponent;

    private void Update()
    {
        textComponent.text = pressureControl.GetControlValue().ToString();
    }
}
