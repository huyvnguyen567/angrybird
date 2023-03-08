using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 6;
    public List<GameObject> enemyPools;
    void Start()
    {
        enemyPools = new List<GameObject>();
        for(int i=0; i< poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPools.Add((enemy));
        }
        GetRandomEnemy();
    }

    public void GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, poolSize);
        enemyPools[randomIndex].transform.position = transform.position;
        enemyPools[randomIndex].SetActive(true);
    }

}
