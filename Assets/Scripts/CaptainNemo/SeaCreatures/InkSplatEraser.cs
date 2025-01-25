using CaptainNemo.Controls;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace CaptainNemo.SeaCreature
{
    public delegate void OnSplatErasedEventHandler();
    public class InkSplatEraser : ControlHandler
    {
        [SerializeField] private float eraserRotationSpeed = 25f;
        [SerializeField] private float eraserRotationMaxAngle = 77f;
        [SerializeField] private Transform eraserBody;
        public Vector3 referenceAxis = Vector3.down;
        
        private Quaternion eraserMaxRotation;
        private bool isGoingLeft = true;
        
        public event OnSplatErasedEventHandler OnSplatErased;

        protected override void OnControl(Vector2 value)
        {
            Camera mainCam = Camera.main;
        
            if (mainCam != null)
            {
                Vector2 mousePos = UnityEngine.Input.mousePosition;
                Vector3 toLookAt = new Vector3(mousePos.x, mousePos.y, -mainCam.transform.position.z);
                Vector3 toMouse = toLookAt - transform.position;
                Vector3 computedPos = mainCam.ScreenToWorldPoint(toMouse);

                float mouseAngle = Vector3.SignedAngle(referenceAxis, computedPos - transform.position, Vector3.forward);
                float eraserAngle = Vector3.SignedAngle(referenceAxis, eraserBody.up, Vector3.forward);

                if (Mathf.Abs(mouseAngle) - Mathf.Abs(eraserAngle) > 90)
                {
                    Release();
                }
            
                if (isGoingLeft)
                {
                    if (mouseAngle > eraserAngle)
                    {
                        return;
                    }

                    if (mouseAngle < -eraserRotationMaxAngle)
                    {
                        OnSplatErased?.Invoke();
                        isGoingLeft = false;
                        return;
                    }
                
                }else
                {
                    if (mouseAngle < eraserAngle)
                    {
                        return;
                    }

                    if (mouseAngle > eraserRotationMaxAngle)
                    {
                        OnSplatErased?.Invoke();
                        isGoingLeft = true;
                        return;
                    }
                }
                transform.LookAt(computedPos);
            }
        }

        public override GlobalControlParam GetGlobalControlParam()
        {
            GlobalControlParam globalControlParam = new GlobalControlParam();
            return globalControlParam;
        }
    }
}
