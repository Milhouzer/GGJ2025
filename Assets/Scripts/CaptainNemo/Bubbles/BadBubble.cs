using CaptainNemo.Bubbles;
using Unity.Hierarchy;
using UnityEngine;

public class BadBubble : Bubble
{
    [SerializeField] private float timeToDestroy = 1f;

    private float counterToDestroyBubble = default;

    protected override void Update()
    {
        base.Update();

        counterToDestroyBubble += Time.deltaTime;

        if (counterToDestroyBubble > timeToDestroy)
            Destroy(gameObject);
    }


}
