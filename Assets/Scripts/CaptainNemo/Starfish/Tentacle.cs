using System;
using System.Collections;
using CaptainNemo.Controls;
using UnityEngine;

public delegate void OnTentacleDeathEventHandler(Tentacle sender);
public class Tentacle : ControlHandler
{
    private int _hp = 1;
    private float _hitShakeStrength = 1;
    public event OnTentacleDeathEventHandler onTentacleDeath;
    

    public void Initialize(int hp, float hitShakeStrength)
    {
        _hp = hp;
        _hitShakeStrength = hitShakeStrength;
    }

    public override GlobalControlParam GetGlobalControlParam()
    {
        GlobalControlParam globalControlParam = new GlobalControlParam();
        return globalControlParam;
    }

    protected override void OnHandle()
    {
        _hp--;
        
        if (_hp <= 0)
        {
            onTentacleDeath?.Invoke(this);
            Release();
            Destroy(gameObject);
        }
        
    }
}