using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo.SeaCreatures
{
    public delegate void OnTentacleDeathEventHandler(Tentacle sender);

    public class Tentacle : ControlHandler
    {
        [SerializeField] private int tentacleHp;
        [SerializeField] private Transform visual;
        [SerializeField] private Transform deadVisual;
        [SerializeField] private float _hitShakeStrength = .5f;
        public event OnTentacleDeathEventHandler OnDie;

        public override GlobalControlParam GetGlobalControlParam()
        {
            return 0;
        }

        protected override void OnHandle()
        {
            tentacleHp--;
        
            if (tentacleHp <= 0)
            {
                SoundManager.PlaySound(E_Sound.StarfishLegPop);

                OnDie?.Invoke(this);
                Release();
                visual.gameObject.SetActive(false);
                deadVisual.gameObject.SetActive(true);
            }
        }
    }
}