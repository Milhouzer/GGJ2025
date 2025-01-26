using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo.SeaCreature
{
    public delegate void OnTentacleDeathEventHandler(Tentacle sender);

    public class Tentacle : ControlHandler
    {
        [SerializeField] private int tentacleHp;
        [SerializeField] private Sprite deadTentacleSprite;
        [SerializeField] private SpriteRenderer tentacleSpriteRenderer;
        private float _hitShakeStrength = 1;
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
                OnDie?.Invoke(this);
                Release();
                tentacleSpriteRenderer.sprite = deadTentacleSprite;
            }
        }
    }
}