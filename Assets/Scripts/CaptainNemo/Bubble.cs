using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody = default;



    private void Awake()
    {
        _rigidbody.AddForce(new Vector2(0.1f, -2f));
    }
}
