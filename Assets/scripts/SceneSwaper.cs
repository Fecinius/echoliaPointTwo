using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwaper : MonoBehaviour
{
    public fallDdetection fallDdetection;
    public bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {

    }
 
    // Update is called once per frame
    void Update()
    {
        isFalling = fallDdetection.IsFalling();
        if (fallDdetection.IsFalling() == true)
        {
            SceneManager.LoadScene(1);
            
        }
    }

}
