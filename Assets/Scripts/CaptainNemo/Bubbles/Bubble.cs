using System.Collections;
using Unity.VisualScripting;
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

        [SerializeField] private ParticleSystem deathParticle;
        [SerializeField] private Color colorDeathParticle;

        private const string CHANGE_DIRECTION_COROUTINE_NAME = "ChangeDirection";
        private const string TAG = "Bubble";

        public float Oxygen { get; set; } = 100f;

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

		virtual protected void Update()
        {
            transform.position += new Vector3(Mathf.Cos(currentDirectionAngle), Mathf.Sin(currentDirectionAngle)) * BubblesManager.Instance.CurrentAmplitude * Time.deltaTime;

            currentDirectionAngle = Mathf.MoveTowards(currentDirectionAngle, targetDirectionAngle, BubblesManager.Instance.CurrentRotationSpeed);

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
            targetDirectionAngle += Random.Range(BubblesManager.Instance.CurrentTargetRotationChangeRange.x, BubblesManager.Instance.CurrentTargetRotationChangeRange.y) * Mathf.Deg2Rad;
            yield return new WaitForSeconds(BubblesManager.Instance.CurrentTimeBetweenChangeTargetDirection);
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

                if (Oxygen == otherBubble.Oxygen && 100 - (Oxygen * 2) >= 0f)
                    OnTryMerge?.Invoke(this, otherBubble);
            }
        }

        private void Destroy()
        {
			ParticleSystem.MainModule settings = deathParticle.main;
			settings.startColor = colorDeathParticle;
            deathParticle.Play();
        }

        private void OnDestroy()
        {
            Destroy();
            StopCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
        }
    }
}
