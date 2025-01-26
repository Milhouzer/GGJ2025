using System;
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
        [SerializeField] private float aimBuggedCompensation = 225f;
        public Vector3 referenceAxis = Vector3.down;
        
        private Quaternion eraserMaxRotation;
        private bool isGoingLeft = false;
        
        public event OnSplatErasedEventHandler OnSplatErased;

        private void OnEnable()
        {
            referenceAxis = -new Vector3(Mathf.Cos( (-eraserRotationMaxAngle - 90)/2), Mathf.Sin((eraserRotationMaxAngle - 90)/2), 0);
        }

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
                float eraserAngle = Vector3.SignedAngle(referenceAxis, transform.up, Vector3.forward);
                
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

                    if (eraserAngle < -eraserRotationMaxAngle)
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

                    if (eraserAngle > eraserRotationMaxAngle)
                    {
                        OnSplatErased?.Invoke();
                        isGoingLeft = true;
                        return;
                    }
                }
                transform.rotation = Quaternion.AngleAxis(mouseAngle + aimBuggedCompensation, transform.forward);
            }
        }

        public override GlobalControlParam GetGlobalControlParam()
        {
            GlobalControlParam globalControlParam = new GlobalControlParam();
            return globalControlParam;
        }
    }
}