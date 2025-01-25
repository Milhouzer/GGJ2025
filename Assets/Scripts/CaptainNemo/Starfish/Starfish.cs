using System;
using System.Collections.Generic;
using UnityEngine;

public class Starfish : MonoBehaviour
{
    [SerializeField]private Transform tentaclePrefab;
    [SerializeField]private int nTentacle;
    [SerializeField]private float tentacleRadius;
    [SerializeField]private int tentacleHp;
    [SerializeField]private float tentacleShakeStrength;
    
    private List<Transform> _tentacles = new List<Transform>();
        
    private void Start()
    {
        for (int i = 0; i < nTentacle; i++)
        {
            float angle = 2 * MathF.PI / nTentacle * i;
            float vertical = MathF.Sin(angle);
            float horizontal = MathF.Cos(angle); 
            
            Vector3 spawnDir = new Vector3 (horizontal, vertical, 0);
            Vector3 pos = transform.position;
            Vector3 spawnPos = pos + spawnDir * tentacleRadius;
            
            Transform spawnedTentacleTransform = Instantiate(tentaclePrefab, spawnPos, Quaternion.identity);
            _tentacles.Add(spawnedTentacleTransform);
            Tentacle spawnedTentacle = spawnedTentacleTransform.GetComponent<Tentacle>();
            spawnedTentacle.Initialize(tentacleHp, tentacleShakeStrength);
            spawnedTentacle.onTentacleDeath += OnTentacleDeath;
            _tentacles[i].LookAt(pos);
        }
    }
    
    

    private void OnTentacleDeath(Tentacle sender)
    {
        sender.onTentacleDeath -= OnTentacleDeath;
        _tentacles.Remove(sender.transform);
        
        if (_tentacles.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
