using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int rows = 3;
    public int columns = 5;
    public float spacing = 1.5f;
    public float diveInterval = 5f;

    void Start()
    {
        SpawnEnemies();
        StartCoroutine(StartEnemyDive());
    }

    // Update is called once per frame
    void SpawnEnemies()
    {
        Vector3 startPosition = new Vector3(-columns / 2f * spacing, 4, 0);

        for (int row = 0; row < rows; row++) 
        {
            for (int col = 0; col < columns; col++) 
            {
                Vector3 position = startPosition + new Vector3(col * spacing, -row * spacing, 0);
                Instantiate(enemyPrefab, position, Quaternion.identity);
            }
        }
    }

    IEnumerator StartEnemyDive() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(diveInterval);

            // chose a select an enemy to dive
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0) 
            {
                GameObject divingEnemy = enemies[Random.Range(0, enemies.Length)];
                divingEnemy.GetComponent<Enemy>().StartDive();
            }
        }
    }
}
