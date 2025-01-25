using CaptainNemo.Controls;
using Unity.VisualScripting;
using UnityEngine;

public class InkSplatEraser : ControlHandler
{
    [SerializeField] private float eraserRotationSpeed = 25f;

    private Quaternion eraserInitialRotation;
    private Quaternion eraserMaxRotation;

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
            Vector3 toLookAt = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, -mainCam.transform.position.z);
            transform.LookAt(mainCam.ScreenToWorldPoint(toLookAt));
        }
    }

    public override GlobalControlParam GetGlobalControlParam()
    {
        GlobalControlParam globalControlParam = new GlobalControlParam();
        return globalControlParam;
    }
}
