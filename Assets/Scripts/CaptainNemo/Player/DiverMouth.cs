using System;
using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo.Player
{
    public class DiverMouth : MonoBehaviour
    {
        [SerializeField] private Transform _mouthStartTransform;
        private Vector3 _mouthStartPosition;
        [SerializeField] private float moveSpeed = 1f;

        private void Awake()
        {
            if (_mouthStartTransform == null) return;
            _mouthStartPosition = _mouthStartTransform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            IControlHandler handler = other.GetComponent<IControlHandler>();
            if (handler == null || handler.GetGlobalControlParam() != GlobalControlParam.Oxygen) return;
            
            handler.Handle();
        }
        
        public void Move(Vector2 move)
        {
            if (move == Vector2.zero)
            {
                transform.position = Vector3.Lerp(transform.position, _mouthStartPosition, moveSpeed);
                return;
            }
            
            transform.position += new Vector3(move.x, move.y) * (moveSpeed * Time.deltaTime);
        }
    }
}
