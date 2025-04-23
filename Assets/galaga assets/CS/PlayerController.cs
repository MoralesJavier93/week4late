using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public GameObject gameOverUI; //this is for the game over ui
    private bool isGameOver = false; // Gave Over state tracker

    void Update()
    {
        // restart if game over
        if (isGameOver) 
        {
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                RestartGame(); // restart game by pressing "R"
            }
            return;
        }

        HandleMovement();
        HandleShooting();
    }

    void HandleMovement() 
    {
        float moveX = Input.GetAxis("Horizontal"); // R 2 L movement
        Vector3 movement = new Vector3(moveX, 0, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
        
    }

    void HandleShooting() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) // shooting 
        {
            Shoot();
        }
    }


    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Bullet Prefab is not assigned in PlayerController!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") && !isGameOver) 
        {
            GameOver(); // if you crash into the enemy
        }
    }

    void GameOver() 
    {
        isGameOver = true;
        
        if (gameOverUI != null) 
        {
            gameOverUI.SetActive(true); // shows game over ui
        }

        Invoke("PauseGame", 0.5f);
    }

    void PauseGame() 
    {
        Time.timeScale = 0;
    }

    public void RestartGame() 
    {
        Time.timeScale = 1; // resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload current scene
    }

}
