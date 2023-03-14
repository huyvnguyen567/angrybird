using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    private static BirdSpawner instance;
    [SerializeField] GameObject[] birdPrefab;
    [SerializeField] int birdIndexSelected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static BirdSpawner Instance
    {
        get { return instance; }
    }
    private void Start()
    {
        birdIndexSelected = PlayerPrefs.GetInt(Constants.BIRDINDEX);
        SpawnBird();
    }

    public void SpawnBird()
    {
        Instantiate(birdPrefab[birdIndexSelected], transform.position, transform.rotation);
    }
}
