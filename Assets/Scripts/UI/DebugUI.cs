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
        pressureValue.text = $"Pressure: {Diver.PressureLevel}";
        oxygenValue.text = $"Oxygen: {Diver.OxygenLevel}";
        temperatureValue.text = $"Temperature: {Diver.TemperatureLevel}";
    }
}
