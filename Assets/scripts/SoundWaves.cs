using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundWaveReveal : MonoBehaviour
{
    public Material revealMaterial;  // Assign RevealMaterial in Inspector
    public float revealRadius = 3f;  // How big the reveal is
    public float fadeTime = 2f;      // How fast the effect fades
   
    public Vector2 playerPosition;

    void Start()
    {
        revealMaterial.SetFloat("_RevealRadius", 0f); // Start invisible
        playerPosition = transform.position; // Set player position
    }

    void Update()
    {
        // Detect mouse click and trigger sound wave
        if (Input.GetMouseButtonDown(0))  // 0 is for left mouse button
        {
            // Get mouse position in screen space
            Vector2 mousePosition = Input.mousePosition;
            TriggerSoundWave(mousePosition);
        }
    }

    public void TriggerSoundWave(Vector2 mousePosition)
    {
        StopAllCoroutines(); // Reset any previous fades
        StartCoroutine(ExpandWave(mousePosition));
    }

    IEnumerator ExpandWave(Vector2 mousePosition)
    {
        // Convert mouse position from screen space to world space
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from player to mouse
        Vector2 direction = (mouseWorldPos - playerPosition).normalized;

        // The maximum distance for the wave (this will be the reveal radius)
        float maxRadius = revealRadius;

        // Set the center of the reveal to be the player position
        revealMaterial.SetVector("_RevealCenter", new Vector4(playerPosition.x, playerPosition.y, 0, 0));

        // Pass the direction to the shader if it handles directional waves
        revealMaterial.SetVector("_RevealDirection", new Vector4(direction.x, direction.y, 0, 0));

        // Expand the wave over time
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            // Calculate the current radius based on time
            float radius = Mathf.Lerp(0, maxRadius, t / fadeTime);

            // Update the reveal radius in the material
            revealMaterial.SetFloat("_RevealRadius", radius);

            yield return null; // Wait for the next frame
        }

        // Ensure the final radius is set to maxRadius
        revealMaterial.SetFloat("_RevealRadius", maxRadius);
    }
}
