using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opacity : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
       spriteRenderer = GetComponent <SpriteRenderer>();
       //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
