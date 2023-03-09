using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject[] birdPrefab;
    int birdIndexSelected;
    private void Start()
    {
        birdIndexSelected = PlayerPrefs.GetInt("birdIndex");
        SpawnBird();
    }

    public void SpawnBird()
    {
        Instantiate(birdPrefab[birdIndexSelected], transform.position, transform.rotation);
    }
}
