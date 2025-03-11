using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundWaveReveal : MonoBehaviour
{
    public Material revealMaterial; // Assign the material in the Inspector
    public Transform player; // Assign the player GameObject in the Inspector
    public float revealRadius = 0f; // Maximum reveal radius
    public float revealTime = 0f; // Time for the wave to expand & fade

    private void Start()
    {
        if (revealMaterial == null)
        {
            Debug.LogError("Reveal Material is not assigned!");
            return;
        }

        revealMaterial.SetFloat("_RevealRadius", 0f); // Start fully hidden
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button triggers a sound wave
        {
            Vector2 mousePosition = Input.mousePosition;
            TriggerSoundWave(mousePosition);
        }
    }

    public void TriggerSoundWave(Vector2 mousePosition)
    {
        StopAllCoroutines(); // Stop any ongoing wave expansion
        StartCoroutine(ExpandAndFadeWave(mousePosition));
    }

    private IEnumerator ExpandAndFadeWave(Vector2 mousePosition)
    {
        // Convert mouse position from screen to world coordinates
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        // Get the player's current position (fixing the static issue)
        Vector2 playerPosition = player.position;

        // Calculate direction from the player to the mouse
        Vector2 direction = (mouseWorldPos - playerPosition).normalized;

        // Set shader properties
        revealMaterial.SetVector("_RevealCenter", new Vector4(playerPosition.x, playerPosition.y, 0, 0));
        revealMaterial.SetVector("_RevealDirection", new Vector4(direction.x, direction.y, 0, 0));
        revealMaterial.SetFloat("_RevealRadius", 0); // Start fully hidden

        // Expand the wave
        for (float t = 0; t < revealTime; t += Time.deltaTime)
        {
            float radius = Mathf.Lerp(0, revealRadius, t / revealTime);
            revealMaterial.SetFloat("_RevealRadius", radius);
            yield return null;
        }

        revealMaterial.SetFloat("_RevealRadius", revealRadius); // Ensure max radius is applied

        // Wait briefly, then fade the wave back to invisible
        yield return new WaitForSeconds(4f);
        float fadeTime = revealTime * 3f;
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            float radius = Mathf.Lerp(revealRadius, 0, t / fadeTime);
            revealMaterial.SetFloat("_RevealRadius", radius);
            yield return null;
        }

        revealMaterial.SetFloat("_RevealRadius", 0f); // Fully hidden again
    }
}
