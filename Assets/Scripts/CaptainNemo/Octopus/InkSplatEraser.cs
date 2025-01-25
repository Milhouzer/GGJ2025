using System.Numerics;
using CaptainNemo.Controls;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class InkSplatEraser : ControlHandler
{
    [SerializeField] private float eraserRotationSpeed = 25f;
    [SerializeField] private float eraserRotationMaxAngle = 77f;
    [SerializeField] private Transform eraserBody = default;

    private Quaternion eraserInitialRotation;
    private Quaternion eraserMaxRotation;

    private bool isGoingRight = true;

    protected override void OnHandle()
    {
        eraserInitialRotation = transform.rotation;
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

            float mouseAngle = Vector3.SignedAngle(Vector3.down, computedPos - transform.position, Vector3.forward);
            float eraserAngle = Vector3.SignedAngle(Vector3.down, eraserBody.up, Vector3.forward);

            if (Mathf.Abs(mouseAngle) - Mathf.Abs(eraserAngle) > 90)
            {
                Release();
            }
            
            if (isGoingRight)
            {
                if (mouseAngle > eraserAngle)
                {
                    return;
                }

                if (mouseAngle < -eraserRotationMaxAngle)
                {
                    isGoingRight = false;
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
                    isGoingRight = true;
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
