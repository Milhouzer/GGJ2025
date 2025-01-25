using System;
using CaptainNemo.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public ControlHandler pressureControl;
    public ControlHandler temperatureControl;
    public ControlHandler oxygenControl;
    
    public TextMeshProUGUI pressureValue;
    public TextMeshProUGUI temperatureValue;
    public TextMeshProUGUI oxygenValue;

    private void Update()
    {
        pressureValue.text = $"Pressure: {pressureControl?.GetControlValue()}";
        oxygenValue.text = $"Oxygen: {oxygenControl?.GetControlValue()}";
        temperatureValue.text = $"Pressure: {temperatureControl?.GetControlValue()}";
    }
}
