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

        [SerializeField] private SpriteRenderer mouthSprite;
        [SerializeField] private Sprite defaultMouth;
        [SerializeField] private Sprite upMouth;
        [SerializeField] private Sprite leftMouth;
        [SerializeField] private Sprite rightMouth;

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
            SetMouthSprite(move);
            if (move == Vector2.zero)
            {
                transform.position = Vector3.Lerp(transform.position, _mouthStartPosition, moveSpeed);
                return;
            }
            
            
            transform.position += new Vector3(move.x, move.y) * (moveSpeed * Time.deltaTime);
        }

        private void SetMouthSprite(Vector2 move)
        {
            if (move == Vector2.zero)
            {
                mouthSprite.flipX = false;
                mouthSprite.sprite = defaultMouth;
                return;
            }
            
            if (move.x < 0)
            {
                mouthSprite.flipX = true;
                mouthSprite.sprite = leftMouth;
                return;
            }

            if (move.x > 0)
            {
                mouthSprite.flipX = false;
                mouthSprite.sprite = rightMouth;
                return;
            }
            
            if (move.y <= 0)
            {
                mouthSprite.flipX = false;
                mouthSprite.sprite = defaultMouth;
                return;
            }

            if (move.y > 0)
            {
                mouthSprite.flipX = false;
                mouthSprite.sprite = upMouth;
            }
        }
    }
}
