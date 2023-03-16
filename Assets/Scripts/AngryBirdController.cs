using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AngryBirdController : MonoBehaviour
{
    [SerializeField] private AngryBirdModel model;
    [SerializeField] private AngryBirdView view;
    [SerializeField] private BirdSpawner birdSpawner;
    [SerializeField] private EnemyPooling enemyPooling;
    [SerializeField] private Slider sliderSound;
    [SerializeField] private GameObject smokePref;

    private float timeForBird = 0f;

    public BirdController Bird { get; set; }

    void Start()
    {
        AudioListener.volume = sliderSound.value;

        if (!PlayerPrefs.HasKey(Constants.VOLUME))
        {
            PlayerPrefs.SetFloat(Constants.VOLUME, 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }
    private void Update()
    {
        if(Bird.HasBirdDied == true)
        {
            timeForBird += Time.deltaTime;
            if (timeForBird>= 2)
            {
                UpdateNewBird();
                Bird.HasBirdDied = false;
                timeForBird = 0;
            }    
           
        }
        if(Bird.HasEnemyDied == true)
        {
            SpawnEffect();
            UpdateNewEnemy();
            Bird.HasEnemyDied = false;
        }    
        if(Bird.DrawLine == true && Bird.HasBirdDied ==false)
        {
            PredictionLine();
        }    
    }
    public void ChangeVolume()
    {
        AudioListener.volume = sliderSound.value;
        SaveVolume();
    }
    public void LoadVolume()
    {
        sliderSound.value = PlayerPrefs.GetFloat(Constants.VOLUME);
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(Constants.VOLUME, sliderSound.value);
    }
    public void OnClickSetting(bool state)
    {
        view.StateSetting(state);
    }

    public void UpdateNewBird()
    {
        Destroy(Bird);
        birdSpawner.SpawnBird();
    }
    public void UpdateNewEnemy()
    { 
        GameObject enemy = enemyPooling.GetPoolObject();
        if (enemy != null)
        {
            enemy.transform.position = new Vector3(1.45f, 2.07f, 0);
            enemy.SetActive(true);
        }
    }
    public void SpawnEffect()
    {
        GameObject smoke = Instantiate(smokePref, Bird.transform.position, Quaternion.identity);
        Destroy(smoke, 0.8f);
    }

    Vector3 PointPosition(float t)
    {
        Vector3 position = Bird.StartPosition + (Bird.Direction1.normalized * model.LaunchForce * t) + 0.5f * Physics2D.gravity * Mathf.Pow(t, 2); ;
        return position;
    }
    void PredictionLine()
    {
        view.ShowPredictionLine(true);
        var points = new List<Vector3>();
        for (int i = 0; i < model.NumberOfPoints; i++)
        {
            points.Add(PointPosition(i * model.SpaceBetweenPoint));
        }
        Bird.ShowLine(points);

    }    

   

}
