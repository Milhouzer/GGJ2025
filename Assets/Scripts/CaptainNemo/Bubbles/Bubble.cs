using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        
        public const float MaxBubbleOxygen = 100f;
        [field: SerializeField][Range(-MaxBubbleOxygen, MaxBubbleOxygen)] public float Oxygen { get; set; } = 100f;

        [HideInInspector] public float targetDirectionAngle;
        [HideInInspector] public float currentDirectionAngle;

        // For test
        // [SerializeField] private float mouthSuckForce = 0.01f;

        public event BubbleEventHandler OnTryMerge;

        public void Awake()
        {
            transform.localScale = Vector3.one * Mathf.Abs(Oxygen/MaxBubbleOxygen);
            
            currentDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
            targetDirectionAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

            StartCoroutine(CHANGE_DIRECTION_COROUTINE_NAME);
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
            {
                Vector3 toTransform = (other.transform.position - transform.position).normalized;

                targetDirectionAngle = Mathf.Atan2(-toTransform.y, -toTransform.x);
                currentDirectionAngle = targetDirectionAngle;


                if (other.TryGetComponent<Bubble>(out Bubble otherBubble) && Mathf.Approximately(Oxygen, otherBubble.Oxygen) && 100 - (Oxygen * 2) >= 0f)
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
