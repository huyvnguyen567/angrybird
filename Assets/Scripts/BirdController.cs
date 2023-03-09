using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Vector2 startPosition;
    private Rigidbody2D rb;
    public float speed = 1000;
    private bool isDragging = false;

    private EnemyPooling pooling;
    private BirdSpawner birdSpawner;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoint;
    Vector2 direction1;
    public float launchForce;

    public AudioClip hitAudio;
    private AudioSource audioSource;

    GameManager gameManager;
    public int score = 1;

    public float velocity;

    bool collied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;

        gameManager = FindObjectOfType<GameManager>();

        pooling = FindObjectOfType<EnemyPooling>();
        birdSpawner = FindObjectOfType<BirdSpawner>();

        audioSource = GetComponent<AudioSource>();

        points = new GameObject[numberOfPoints];
        
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, startPosition, Quaternion.identity);
            points[i].SetActive(false);
        }
      
    }
    private void Update()
    {
        velocity = rb.velocity.magnitude;
        
        if (transform.position.y <= -4 || velocity <=1 && transform.position.y <=-2.5)
        {
            Destroy(gameObject);
            birdSpawner.SpawnBird();
        }

        if (isDragging == true)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].SetActive(true);
                points[i].transform.position = PointPosition(i * spaceBetweenPoint);
            }
        }
        if (Vector2.Distance(transform.position, startPosition) > 2)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                Destroy(points[i]);
            }
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = startPosition + (direction1.normalized * launchForce * t) + 0.5f * Physics2D.gravity * Mathf.Pow(t,2); ;
        return position;
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
        Vector2 direction = startPosition - rb.position;
        direction.Normalize();
        rb.AddForce(direction * speed);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = mousePosition;
        if (Vector2.Distance(targetPosition, startPosition) > 2)
        {
            Vector2 direction = targetPosition - startPosition;
            direction.Normalize();
            targetPosition = startPosition + direction * 1.5f;
            direction1 = -direction;
        }
        else
        {
            Vector2 direction = targetPosition - startPosition;
            direction1 = -direction;
        }
        rb.position = targetPosition;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy" && collied == false)
        {
            collied = true;
            audioSource.PlayOneShot(hitAudio);
            collision.gameObject.SetActive(false);
            gameManager.UpdateScore(score);
            if (transform.position.y <= -4 || velocity <= 1 && transform.position.y <= -2.5)
            {
                
                Destroy(gameObject);
                birdSpawner.SpawnBird();
                
            } 
            else
            {
                if (!collision.gameObject.activeInHierarchy)
                {
                    pooling.GetRandomEnemy();
                }
            }
        }        
    }
}
