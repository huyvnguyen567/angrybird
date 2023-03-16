using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private AudioClip hitAudio;

    public Vector2 StartPosition { get; set; }
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public Vector2 Direction1 { get; set; }
    private bool collied = false;
    private bool isDragging = false;
    public bool HasBirdDied { get; set; }
    public bool HasEnemyDied { get; set; }
    public bool DrawLine { get; set; }


    public void ActiveLine(bool isActive)
    {
        line.enabled = isActive;
    }

    public float Speed { get; set; }
    public int NumberOfPoints { get; set; }
    public float SpaceBetweenPoint { get; set; }
    public float LaunchForce { get; set; }
    public int Score { get; set; }
    public float Velocity { get; set; }
    //public GameManager GameManager { get; set; }
    public BirdSpawner BirdSpawner { get; set; }
    public EnemyPooling EnemyPooling { get; set; }

    public AngryBirdAudio AngryBirdAudio { get; set; }
    public AngryBirdView AngryBirdView { get; set; }
    public AngryBirdController AngryBirdController { get; set; }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
        HasBirdDied = false;
        HasEnemyDied = false;
        DrawLine = false;
    }

    void Start()
    {
        StartPosition = rb.position;
    }
    private void Update()
    {

        Velocity = rb.velocity.magnitude;
        
        if (transform.position.y <= -4 || Velocity <=1 && transform.position.y <=-2.5)
        {
            Destroy(gameObject);
            HasBirdDied = true;
        }

        if (isDragging == true)
        {
            DrawLine = true;    
        }
        if (Vector2.Distance(transform.position, StartPosition) > 2.0f)
        {
            AngryBirdView.ShowPredictionLine(false);
        }
    }
    
    
    private void OnMouseDown()
    {
        isDragging = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
        Vector2 direction = StartPosition - rb.position;
        direction.Normalize();
        rb.AddForce(direction * Speed);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = mousePosition;
        if (Vector2.Distance(targetPosition, StartPosition) > 2.0f)
        {
            Vector2 direction = targetPosition - StartPosition;
            direction.Normalize();
            targetPosition = StartPosition + direction * 1.5f;
            Direction1 = -direction;
        }
        else
        {
            Vector2 direction = targetPosition - StartPosition;
            Direction1 = -direction;
        }
        rb.position = targetPosition;

    }

    internal void ShowLine(List<Vector3> points)
    {
        line.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i, points[i]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.ENEMY && collied == false)
        {
            collied = true;
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(AngryBirdAudio.PlayAudioClip());
            AngryBirdView.ShowScore(Score);
            if (transform.position.y <= -4 || Velocity <= 1 && transform.position.y <= -2.5)
            {
                HasBirdDied = true;
                Destroy(gameObject);
            }
            else
            {
                if (!collision.gameObject.activeInHierarchy)
                {
                    HasEnemyDied = true;
                }
            }
        }        
    }
      
}
