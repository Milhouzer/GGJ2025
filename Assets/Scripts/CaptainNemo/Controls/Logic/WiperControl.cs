using System.Collections;
using CaptainNemo.SeaCreatures;
using UnityEngine;

namespace CaptainNemo.Controls.Logic
{
    public delegate void OnSplatErasedEventHandler();
    public class WiperControl : ControlHandler
    {
        [SerializeField] private float returnToInitPosMoveSpeed = 0.1f;
        private Vector3 _initPosition;
        private Coroutine _returnToInitPos;
        
        protected override void OnStart()
        {
            _initPosition = transform.position;
        }

        protected override void OnHandle()
        {
            if (_returnToInitPos != null)
            {
                StopCoroutine(_returnToInitPos);
                _returnToInitPos = null;
            }
        }

        protected override void OnControl(Vector2 value)
        {
            Camera cam = Camera.main;
            if(cam == null) { return; }
            
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Vector3 origin  = ray.origin;
            Vector3 lastPosition = transform.position;
            Vector3 newPosition = new Vector3(origin.x, origin.y, 0);
            transform.position = newPosition;
            
            float wipeAmount = Vector3.Distance(lastPosition, newPosition);
            if (InkSplatSpawner.CurrentInkSplat != null)
            {
                InkSplatSpawner.CurrentInkSplat.Wipe(wipeAmount);
            }
        }

        protected override void OnRelease()
        {
            _returnToInitPos = StartCoroutine(ReturnToInitPos());
        }

        private IEnumerator ReturnToInitPos()
        {
            while (Vector3.Distance(transform.position, _initPosition) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, _initPosition, returnToInitPosMoveSpeed);
                yield return null;
            }

            _returnToInitPos = null;
        }

        public override GlobalControlParam GetGlobalControlParam()
        {
            GlobalControlParam globalControlParam = new GlobalControlParam();
            return globalControlParam;
        }
    }
}
