using CaptainNemo.Bubbles;
using CaptainNemo.Game;
using UnityEngine;

namespace CaptainNemo.Controls.Logic
{
    /// <summary>
    /// Oxygen control handler. This component is attached to a bubble
    /// </summary>
    public class OxygenControl : ControlHandler
    {
        /// <summary>
        /// Current pressure value.
        /// </summary>
        private float _oxygen;

        /// <summary>
        /// This component controls the oxygen
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Oxygen;

        [SerializeField] private Bubble bubble;

		protected override void OnStart()
		{
			base.OnStart();
            _oxygen = bubble.Oxygen;
		}

        protected override void OnHandle()
        {
            base.OnHandle();

            if (_oxygen > 0)
                SoundManager.PlaySound(E_Sound.GoodBubble);
            else
                SoundManager.PlayRandomSound(new() { E_Sound.WrongBubble, E_Sound.WrongBubble2 });

            GameManager.AddOxygen(-_oxygen);
            Release();
            BubblesManager.Instance.DestroyBubble(bubble);
        }

        /// <summary>
        /// Get the actual pressure of the control
        /// </summary>
        /// <returns></returns>
        public override float GetControlValue()
        {
            return _oxygen;
        }
        
        /// <summary>
        /// Controls the oxygen level
        /// </summary>
        /// <returns></returns>
        public override GlobalControlParam GetGlobalControlParam()
        {
            return globalParam;
        }
    }
}