using System;
using CaptainNemo.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class Starfish : MonoBehaviour
{
    [SerializeField]private Transform tentaclePrefab;
    [SerializeField]private int nTentacle;
    [SerializeField]private float tentacleRadius;
    private Transform[] _tentacles;
        
    private void Start()
    {
        _tentacles = new Transform[nTentacle];
        
        for (int i = 0; i < nTentacle; i++)
        {
            float angle = 2 * MathF.PI / nTentacle * i;
            float vertical = MathF.Sin(angle);
            float horizontal = MathF.Cos(angle); 
            
            Vector3 spawnDir = new Vector3 (horizontal, vertical, 0);
            Vector3 pos = transform.position;
            Vector3 spawnPos = pos + spawnDir * tentacleRadius;
            
            _tentacles[i] = Instantiate (tentaclePrefab, spawnPos, Quaternion.identity, transform);
            _tentacles[i].LookAt(pos);
        }
    }
}
