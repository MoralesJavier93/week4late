using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDownAmount = 0.5f;
    private Vector2 moveDirection = Vector2.right;

    public float diveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private bool isDiving = false;
    private Transform player;
    private Vector3 targetPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    
    void Update()
    {
        if (isDiving)
        {
            MoveInFormation();
        }
        else 
        {
            DiveTowardsTarget();
        }
    }

    void MoveInFormation() 
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        float screenwidth = Camera.main.orthographicSize * Camera.main.aspect;
        if (transform.position.x >= screenwidth || transform.position.x <= -screenwidth) 
        {
            moveDirection *= -1;
            transform.position += Vector3.down * moveDownAmount;
        }
    }

    public void StartDive() 
    {
        isDiving = true;
        if (player != null)
        {
            targetPosition = player.position + new Vector3(Random.Range(-1f, 1f), 0, 0);
        }
        else 
        {
            //dive randomly if no player
            targetPosition = new Vector3(Random.Range(-5f, 5f), -5f, 0);
        }

        InvokeRepeating("shoot", 0.5f, 1.5f);
    }

    void DiveTowardsTarget() 
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, diveSpeed * Time.deltaTime);

        if (transform.position == targetPosition) 
        {
            CancelInvoke("shoot");
            Destroy(gameObject); // destory after dive wasnt about to figure out how to make it return
        }
    }

    void Shoot() 
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) 
        {
           Destroy(gameObject);
        }
    }
}
