using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    [SerializeField] private AngryBirdModel model;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawner;
    public int PoolSize { get; set; }
    private List<GameObject> enemyPools;

    private void Awake()
    {
        PoolSize = model.PoolSize;
    }
    void Start()
    {
        enemyPools = new List<GameObject>();
        for(int i=0; i< PoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemy.transform.SetParent(enemySpawner.transform);
            enemyPools.Add((enemy));
        }

        GameObject initEnemy = GetPoolObject();
         initEnemy.transform.position = enemySpawner.transform.position;
         initEnemy.SetActive(true);
    }

   
    public GameObject GetPoolObject()
    {
        for(int i=0; i<enemyPools.Count; i++)
        {
            if(!enemyPools[i].activeInHierarchy)
            {
                return enemyPools[i];
            }    
        }
        return null;
    }

}
