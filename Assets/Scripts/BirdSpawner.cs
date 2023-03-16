using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private BirdController[] birdPrefab;

    //[SerializeField] GameManager gameManager;
    [SerializeField] private BirdSpawner birdSpawner;
    [SerializeField] private EnemyPooling enemyPooling;

    [SerializeField] private AngryBirdAudio Audio;
    [SerializeField] private AngryBirdView view;
    [SerializeField] private AngryBirdModel model;
    [SerializeField] private AngryBirdController controller;

    private int birdIndexSelected;


    private void Start()
    {
        birdIndexSelected = PlayerPrefs.GetInt(Constants.BIRDINDEX);
        SpawnBird();
    }

    public void SpawnBird()
    {
        var bird = Instantiate(birdPrefab[birdIndexSelected], transform.position, transform.rotation);
        //bird.GameManager = gameManager;
        bird.BirdSpawner = birdSpawner;
        bird.EnemyPooling = enemyPooling;
        bird.AngryBirdAudio = Audio;
        bird.AngryBirdView = view;
        bird.AngryBirdController = controller;
        

        //Lay data tu script model
        bird.Speed = model.Speed;
        bird.NumberOfPoints = model.NumberOfPoints;
        bird.SpaceBetweenPoint = model.SpaceBetweenPoint;
        bird.LaunchForce = model.LaunchForce;
        bird.Score = model.Score;
        bird.Velocity = model.Velocity;

        controller.Bird = bird;
        view.Bird = bird;
    }
}
