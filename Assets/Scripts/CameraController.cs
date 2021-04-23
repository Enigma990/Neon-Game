using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 offset;
    private Vector3 camerPos;

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - lookAt.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //New Position
        camerPos = lookAt.position + offset;

        //Restricting x movement
        camerPos.x = 0;
    }
    private void LateUpdate()
    {
        transform.position = camerPos;
    }
}
