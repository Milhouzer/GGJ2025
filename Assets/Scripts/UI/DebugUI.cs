using CaptainNemo.Player;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public Diver Diver;
    
    public TextMeshProUGUI pressureValue;
    public TextMeshProUGUI temperatureValue;
    public TextMeshProUGUI oxygenValue;

    private void Update()
    {
        pressureValue.text = $"Pressure: {Diver.PressureParam.Value}";
        oxygenValue.text = $"Oxygen: {Diver.OxygenParam.Value}";
        temperatureValue.text = $"Temperature: {Diver.TemperatureParam.Value}";
    }
}
