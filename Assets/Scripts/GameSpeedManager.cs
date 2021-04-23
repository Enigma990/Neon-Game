using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private SpawnEnemies spawnEnemies = null;

    private int scoreLimit;

    // Start is called before the first frame update
    void Start()
    {
        scoreLimit = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.Score > scoreLimit && spawnEnemies.spawnTime > 0.75f)
        {
            spawnEnemies.spawnTime -= 0.2f;
            spawnEnemies.gameObject.transform.position = new Vector3(spawnEnemies.gameObject.transform.position.x, spawnEnemies.gameObject.transform.position.y, spawnEnemies.gameObject.transform.position.z - 1);
           // spawnEnemies.EnemySpeed += 2;
            scoreLimit *= 2;
        }
    }
}
