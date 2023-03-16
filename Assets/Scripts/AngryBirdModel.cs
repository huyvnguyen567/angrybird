using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdModel : MonoBehaviour
{
    
    [Header("Bird")]
    [SerializeField] private float speed = 750.0f;
    [SerializeField] private int score = 1;
    [SerializeField] private float velocity = 0f;

    [Header("Trajectory")]
    [SerializeField] private int numberOfPoints = 20;
    [SerializeField] private float spaceBetweenPoint = 0.05f;
    [SerializeField] private float launchForce = 13.5f;

    [Header("Enemy")]
    [SerializeField] private int poolSize = 6;


    public float Speed => speed;
    public int NumberOfPoints => numberOfPoints;
    public float SpaceBetweenPoint => spaceBetweenPoint;
    public float LaunchForce => launchForce;
    public int Score => score;
    public float Velocity => velocity;
    public int PoolSize => poolSize;

}
