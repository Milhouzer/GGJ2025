using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //For test
    public BubbleManager bubbleManager = default;
    //

    private const string WAIT_COROUTINE_NAME = "WaitBeforeAddForce";

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody = default;

    public float CurrentTimeBetweenAddForce { get; set; }
    public float CurrentRandomSpeed { get; set; }
    public Vector2 CurrentRandomRotationRange { get; set; }
    public float CurrentMouthStrength { get; set; }
    public float CurrentBubbleSize { get; set; }

    //bouge en random, se fais aspirer par la bouche, se divise et se recréée, bouge plus rapidement et plus random quand ça bout)
    //se fais aspirer par la bouche
    //se divise et se recréee
    //bouge plus rapidement et plus random selon la chaleur

    private float speedPhase;

    private float currentAngle;

    public void Init(float startBubbleSize)
    {
        CurrentBubbleSize = startBubbleSize;

        currentAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        speedPhase = Random.Range(0f, 100f);

        StartCoroutine(WAIT_COROUTINE_NAME);
    }

    private void Update()
    {
        if (transform.position.magnitude > BubbleManager.fieldRay)
        {
            transform.position = transform.position.normalized * BubbleManager.fieldRay;
            _rigidbody.linearVelocity = -transform.position.normalized * _rigidbody.linearVelocity.magnitude / 2;
        }
    }

    private IEnumerator WaitBeforeAddForce()
    {
        float angleRadiant = currentAngle + Random.Range(CurrentRandomRotationRange.x, CurrentRandomRotationRange.y) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleRadiant), Mathf.Sin(angleRadiant));

        float speed = Mathf.PerlinNoise(Time.time, speedPhase) * CurrentRandomSpeed;

        _rigidbody.AddForce(direction * speed);

        yield return new WaitForSeconds(CurrentTimeBetweenAddForce);

        StartCoroutine(WAIT_COROUTINE_NAME);
    }

    public void GetSucked(Vector2 succionForce)
    {
        _rigidbody.AddForce(succionForce);
    }

    private void OnDestroy()
    {
        StopCoroutine(WAIT_COROUTINE_NAME);
    }
}
