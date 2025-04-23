using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 6f;


    void Start()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Destory player (or trigger game over)
            Destroy(gameObject);   // destory enemy bullet
        }

        if (other.CompareTag("Fort")) 
        {
            Destroy(other.gameObject); // Destory fort
            Destroy(gameObject);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
