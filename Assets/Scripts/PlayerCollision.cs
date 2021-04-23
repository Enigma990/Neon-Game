using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController parent;
    private bool isColliding = false;

    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerController>();

        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isColliding) return;

        isColliding = true;
        
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            parent.OnChildTriggerEnter(true);
        }
        else
        {
            playerAnim.SetTrigger("isDead");
            parent.OnChildTriggerEnter(false);
        }

        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();

        isColliding = false;
    }

    public void ChangeAnim()
    {
        playerAnim.SetTrigger("isDead");
    }
}
