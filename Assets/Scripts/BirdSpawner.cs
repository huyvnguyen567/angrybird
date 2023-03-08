using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    
    public void SpawnBird()
    {
        Instantiate(birdPrefab, transform.position, transform.rotation);
    }
}
