using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementSound : MonoBehaviour
{
    public movement movement;
    public AudioSource audioSource;
    public float fadeDuration = 0.2f; // Duration of the fade in/out effect
    private float targetVolume = 0f;
    private bool isFading = false;

    void Start()
    {
        movement = GetComponent<movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!movement.CanPlayMovementSound())
        {
            targetVolume = 0f;  // Set the target volume to 0 (silent)
            if (audioSource.isPlaying)
            {
                StopAllCoroutines(); // Stop any fading coroutines that are currently running
                StartCoroutine(FadeOut());
            }
        }
        else
        {
            targetVolume = 1f;  // Set the target volume to 1 (full volume)
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (audioSource.volume < targetVolume)
            {
                StopAllCoroutines(); // Stop any fading coroutines that are currently running
                StartCoroutine(FadeIn());
            }
        }
    }

    // Fade out the audio over time
    private IEnumerator FadeOut()
    {
        isFading = true;
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / fadeDuration*0.4f;
            yield return null;
        }

        isFading = false;
    }

    // Fade in the audio over time
    private IEnumerator FadeIn()
    {
        isFading = true;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
        isFading = false;
    }
}
