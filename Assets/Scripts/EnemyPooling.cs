using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    private static EnemyPooling instance; 
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 6;
    [SerializeField] List<GameObject> enemyPools;
    [SerializeField] GameObject enemySpawner;


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
    public static EnemyPooling Instance
    {
        get { return instance; }
    }
    void Start()
    {
        enemyPools = new List<GameObject>();
        for(int i=0; i< poolSize; i++)
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
