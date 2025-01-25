using System.Collections.Generic;
using UnityEngine;

namespace CaptainNemo.SeaCreature
{
    public delegate void InkSplatManagerEventHandler(InkSplatManager sender);

    public class InkSplatManager : MonoBehaviour
    {
        [SerializeField] private int nSplats = 4;
        [SerializeField] private int nEraseAmountToClean = 4;
        [SerializeField] private Transform inkSplatPrefab;
        [SerializeField] private Transform eraserTransform;
        [SerializeField] private float alphaReduceRate = 0.2f;
        [SerializeField] private float splatRandomScatterStrength = 3f;

        public event InkSplatManagerEventHandler onFinished = default;

        private List<Transform> splats = new List<Transform>();
        private InkSplatEraser eraser;
        private int eraseAmountCounter = 0;

        private void Start()
        {
            eraser = eraserTransform.GetComponent<InkSplatEraser>();
            eraser.OnSplatErased += OnSplatErased;
        }

        public void SpawnSplats()
        {
            for (int i = 0; i < nSplats; i++)
            {
                Transform spawnedInkSplat = Instantiate(inkSplatPrefab, eraserTransform.position + eraser.referenceAxis, Quaternion.identity);
                spawnedInkSplat.position += (Vector3)UnityEngine.Random.insideUnitCircle * splatRandomScatterStrength;
                splats.Add(spawnedInkSplat);
            }
        }

        private void OnSplatErased()
        {
            eraseAmountCounter++;
            int length = splats.Count;
            
            for (int i = 0; i < length; i++)
            {
                SpriteRenderer splatSprite = splats[i].GetComponent<SpriteRenderer>();
                Color splatColor = splatSprite.color;
                splatColor.a -= alphaReduceRate;
                splatSprite.color = splatColor;
                
                //TODO:
                //Add dotween small move towards left or right with small random
            }

            if (eraseAmountCounter >= nEraseAmountToClean)
            {
                eraseAmountCounter = 0;

                for (int i = splats.Count - 1; i >= 0; i--)
                {
                    Destroy(splats[i].gameObject);
                    splats.Remove(splats[i]);
                }

                //Event destroy 
                onFinished?.Invoke(this);
            }
        }
    }
}
