using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemies;   
    public float spawnTime;

    private float currentTime = 0;
    private int enemyNum;
    private GameObject currentEnemy;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        enemyNum = Random.Range(0, enemies.Length);
    }

    // Update is called once per frame
    void Update()
    {
        enemyNum = Random.Range(0, enemies.Length);
        currentEnemy = enemies[enemyNum];

        currentTime += Time.deltaTime;

        if(currentTime > spawnTime)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        currentTime = 0;

        currentEnemy = Instantiate(enemies[enemyNum]);
        currentEnemy.transform.SetParent(transform);
        currentEnemy.transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), transform.localPosition.y, transform.localPosition.z);
    }
}
