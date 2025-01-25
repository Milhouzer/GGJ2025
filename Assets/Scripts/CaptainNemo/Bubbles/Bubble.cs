using System.Collections;
using UnityEngine;

namespace CaptainNemo.Bubbles
{
    public delegate void BubbleEventHandler(Bubble sender, Bubble collidedBubble);
    public class Bubble : MonoBehaviour
    {
        [System.Serializable]
        public struct BubbleMovementRanges
        {
            public Vector2 timeBetweenChangeTargetDirection;
            public Vector2 rotationSpeed;
            public Vector2 amplitude;
            [Space(5)]
            public Vector2 minTargetRotationChangeRange;
            public Vector2 maxTargetRotationChangeRange;
        }

        private const string CHANGE_DIRECTION_COROUTINE_NAME = "ChangeDirection";
        private const string TAG = "Bubble";

        public float Oxygen { get; set; } = 100f;
        public static float CurrentTimeBetweenChangeTargetDirection { get; set; }
        public static float CurrentRotationSpeed { get; set; }
        public static Vector2 CurrentTargetRotationChangeRange { get; set; }
        public static float CurrentAmplitude { get; set; }


        private float currentDirectionAngle;
        private float targetDirectionAngle;

        // For test
        // [SerializeField] private float mouthSuckForce = 0.01f;

        public event BubbleEventHandler OnTryMerge;

        public void Awake()
        {
            currentDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
            targetDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

            StartCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
        }

        private void Update()
        {
            transform.position += new Vector3(Mathf.Cos(currentDirectionAngle), Mathf.Sin(currentDirectionAngle)) * CurrentAmplitude * Time.deltaTime;

            currentDirectionAngle = Mathf.MoveTowards(currentDirectionAngle, targetDirectionAngle, CurrentRotationSpeed);

            if (transform.position.magnitude > BubblesManager.BUBBLE_MOVE_RADIUS)
            {
                transform.position = transform.position.normalized * BubblesManager.BUBBLE_MOVE_RADIUS;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody.tag == TAG)
            {
                Vector3 toTransform = (collision.attachedRigidbody.transform.position - transform.position).normalized;

                targetDirectionAngle = Mathf.Atan2(-toTransform.y, -toTransform.x);
                currentDirectionAngle = targetDirectionAngle;

                Bubble otherBubble = collision.GetComponent<Bubble>();

                if (Oxygen == otherBubble.Oxygen && 100 - (Oxygen * 2) >= TestManager.pressure)
                    OnTryMerge?.Invoke(this, otherBubble);
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
        }
    }
}
