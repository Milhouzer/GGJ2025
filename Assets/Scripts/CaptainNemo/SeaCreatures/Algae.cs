using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaptainNemo.SeaCreatures
{
    public class Algae : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Awake()
        {
            animator.SetFloat("Phase", Random.value);
        }
    }

}