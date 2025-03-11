using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fallDdetection : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool isFalling = false;
    public bool triggerd = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "WorldBottom")
        {
            isFalling = true;
            triggerd = true;
        }
    }
    public bool IsFalling()
    {
        return isFalling;
    }
}

