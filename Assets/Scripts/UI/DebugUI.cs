using CaptainNemo.Game;
using CaptainNemo.Player;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public TextMeshProUGUI pressureValue;
    public TextMeshProUGUI temperatureValue;
    public TextMeshProUGUI oxygenValue;

    private void Update()
    {
        Diver diver = GameManager.GetDiver();
        if (diver == null) return;
        pressureValue.text = $"Pressure: {diver.PressureParam.Value}";
        oxygenValue.text = $"Oxygen: {diver.OxygenParam.Value}";
        temperatureValue.text = $"Temperature: {diver.TemperatureParam.Value}";
    }
}
