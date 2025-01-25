using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private const string CHANGE_DIRECTION_COROUTINE_NAME = "ChangeDirection";

    [SerializeField] private float oxygen = default;
    public float Oxygen { get => oxygen; private set => oxygen = value; }

    public float CurrentTimeBetweenChangeTargetDirection { get; set; } = 0.5f;
    public float CurrentRotationSpeed { get; set; } = 0.03f;
    public Vector2 CurrentTargetRotationChangeRange { get; set; } = new Vector2(-30, 30);
    public float CurrentSpeed { get; set; } = 0.5f;

    
    private float currentDirectionAngle;
    private float targetDirectionAngle;

    //For test
    [SerializeField] private float mouthSuckForce = 0.01f;
    //

    public void Awake()
    {
        currentDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        targetDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

        StartCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
    }

    private void Update()
    {
        transform.position += new Vector3(Mathf.Cos(currentDirectionAngle), Mathf.Sin(currentDirectionAngle)) * CurrentSpeed * Time.deltaTime;

        currentDirectionAngle = Mathf.MoveTowards(currentDirectionAngle, targetDirectionAngle, CurrentRotationSpeed);

        if (transform.position.magnitude > ManagerBubble.FIELD_RAY)
        {
            transform.position = transform.position.normalized * ManagerBubble.FIELD_RAY;
            targetDirectionAngle = Mathf.Atan2(-transform.position.y, -transform.position.x);
            currentDirectionAngle = targetDirectionAngle;
        }

        //////////Test//////////

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
