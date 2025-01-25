using CaptainNemo.Controls;
using UnityEngine;

public class Tentacle : ControlHandler
{
    private int _hp = 1;
    private int _hitShakeStrength = 1;
    
    protected override void OnHandle()
    {
        Debug.Log("yo");
    }
}
