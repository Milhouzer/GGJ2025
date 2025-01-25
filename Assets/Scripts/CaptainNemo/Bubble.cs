using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private const string CHANGE_DIRECTION_COROUTINE_NAME = "ChangeDirection";

    public float CurrentTimeBetweenChangeTargetDirection { get; set; }
    public float CurrentRotationSpeed { get; set; }
    public Vector2 CurrentTargetRotationChangeRange { get; set; }
    public float CurrentSpeed { get; set; }

    private float currentDirectionAngle;
    private float targetDirectionAngle;

    //For test
    [SerializeField] private float mouthSuckForce = 0.01f;
    //

    public void Init()
    {
        currentDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        targetDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

        StartCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
    }

    private void Update()
    {
        transform.position += new Vector3(Mathf.Cos(currentDirectionAngle), Mathf.Sin(currentDirectionAngle)) * CurrentSpeed * Time.deltaTime;

        currentDirectionAngle = Mathf.MoveTowards(currentDirectionAngle, targetDirectionAngle, CurrentRotationSpeed);

        if (transform.position.magnitude > BubbleManager.fieldRay)
        {
            transform.position = transform.position.normalized * BubbleManager.fieldRay;
            targetDirectionAngle = Mathf.Atan2(-transform.position.y, -transform.position.x);
            currentDirectionAngle = targetDirectionAngle;
        }

        //if (UnityEngine.Input.GetKey(KeyCode.Mouse0))
        //{
        //    GetSucked((Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, -Camera.main.transform.position.z)) - transform.position).normalized * mouthSuckForce);
        //}
    }

    private IEnumerator ChangeDirection()
    {
        targetDirectionAngle += Random.Range(CurrentTargetRotationChangeRange.x, CurrentTargetRotationChangeRange.y) * Mathf.Deg2Rad;

        yield return new WaitForSeconds(CurrentTimeBetweenChangeTargetDirection);

        StartCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
    }

    public void GetSucked(Vector3 suckForce)
    {
        transform.position += suckForce;
    }

    private void OnDestroy()
    {
        StopCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
    }
}
