using System;
using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo
{
    public class Diver: MonoBehaviour
    {
        [SerializeField] private Transform mouth;
        private Vector3 _mouthStartPosition;
        
        [SerializeField] private float moveSpeed = 0.1f;

        private void Start()
        {
            _mouthStartPosition = mouth.position;
        }

        public void ParameterCallback(IControlHandler handler)
        {
            Debug.Log($"Parameter {handler.GetGlobalControlParam()} called with value {handler.GetControlValue()} on {this}");
        }

        public void MoveMouth(Vector2 move)
        {
            if (move == Vector2.zero)
            {
                mouth.position = Vector3.Lerp(mouth.position, _mouthStartPosition, moveSpeed);
                return;
            }
            
            mouth.position += new Vector3(move.x, move.y) * (moveSpeed * Time.deltaTime);
        }
    }
}