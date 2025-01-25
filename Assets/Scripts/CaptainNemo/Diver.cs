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
        [SerializeField] private float baseValueOxygen = 100f;
        [SerializeField] private float reduceValueOxygen = 2f;
        [SerializeField] private float timeToReduceValueOxygen = 0.25f;
 
        private float oxygen;
        private float counterReduceValueOxygen;

        public void Oxygen(ControlHandler control)
        {
            float valueOxygenToAdd = control.GetControlValue();
            oxygen += valueOxygenToAdd;
        }

        private void Start()
        {
            _mouthStartPosition = mouth.position;
            oxygen = baseValueOxygen;
        }

		private void Update()
		{
			counterReduceValueOxygen += Time.deltaTime;

            if(counterReduceValueOxygen >= timeToReduceValueOxygen)
            {
                oxygen -= reduceValueOxygen;
                counterReduceValueOxygen = 0;

                if (oxygen <= 0)
                    Death();
            }
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

        private void Death()
        {
            Debug.Log("T'es mort gros naze");
        }
    }
}