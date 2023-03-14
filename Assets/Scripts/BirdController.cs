using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Vector2 startPosition;
    private Rigidbody2D rb;
    [SerializeField] float speed = 1000.0f;
    private bool isDragging = false;

    [SerializeField] LineRenderer line;
    [SerializeField] int numberOfPoints;
    [SerializeField] float spaceBetweenPoint;
    Vector2 direction1;
    [SerializeField] float launchForce;

    [SerializeField] AudioClip hitAudio;
    private AudioSource audioSource;

    [SerializeField] int score = 1;

    [SerializeField] float velocity;

    bool collied = false;

    [SerializeField] GameObject smokePref;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        startPosition = rb.position;
    }
    private void Update()
    {

        velocity = rb.velocity.magnitude;
        
        if (transform.position.y <= -4 || velocity <=1 && transform.position.y <=-2.5)
        {
            Destroy(gameObject);
            BirdSpawner.Instance.SpawnBird();
        }

        if (isDragging == true)
        {
            line.positionCount = numberOfPoints;
            for (int i = 0; i < line.positionCount; i++)
            {
                line.SetPosition(i, PointPosition(i * spaceBetweenPoint));
            }
        }
        if (Vector2.Distance(transform.position, startPosition) > 2.0f)
        {
            line.enabled = false;
            
        }
    }

    Vector3 PointPosition(float t)
    {
        Vector3 position = startPosition + (direction1.normalized * launchForce * t) + 0.5f * Physics2D.gravity * Mathf.Pow(t,2); ;
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
        if (Vector2.Distance(targetPosition, startPosition) > 2.0f)
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
        if(collision.gameObject.tag == Constants.ENEMY && collied == false)
        {
            collied = true;

            audioSource.PlayOneShot(hitAudio);
            collision.gameObject.SetActive(false);
            GameManager.Instance.UpdateScore(score);
            if (transform.position.y <= -4 || velocity <= 1 && transform.position.y <= -2.5)
            {
                
                Destroy(gameObject);
                BirdSpawner.Instance.SpawnBird();
                
            } 
            else
            {
                if (!collision.gameObject.activeInHierarchy)
                {
                    GameObject smoke = Instantiate(smokePref, collision.transform.position, Quaternion.identity);
                    Destroy(smoke, 0.8f);
                    GameObject enemy = EnemyPooling.Instance.GetPoolObject();
                    if(enemy != null)
                    {
                        enemy.transform.position = new Vector3(1.45f,2.07f,0);
                        enemy.SetActive(true);
                    }    
                }
            }
        }        
    }
      
}
