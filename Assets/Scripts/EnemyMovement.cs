using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed;

    private Rigidbody enemyRb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;

        enemyRb = GetComponent<Rigidbody>();
        enemyRb.velocity = new Vector3(0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < Camera.main.transform.position.z * 2)
        {
            Destroy(gameObject);
        }
    }
}
