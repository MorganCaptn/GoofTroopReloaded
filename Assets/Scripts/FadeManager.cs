using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Für den Image-Component, der das Fading steuert

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // UI-Image für das Faden
    public float fadeDuration = 1f; // Dauer des Fadings
    public PlayerMovement playerMovement; // Referenz zu PlayerMovement
    private bool isFading = false;

    [Range(0f, 1f)]
    public float unlockMovementDuringFadeOut = 0.8f; // Zeitpunkt für Entsperrung während des Fade-Out
    [Range(0f, 1f)]
    public float unlockMovementDuringFadeIn = 0.2f; // Zeitpunkt für Entsperrung während des Fade-In

    // Methode zum Einleiten des Fade-Out und Fade-In
    public void FadeOutAndIn(System.Action onFadeComplete)
    {
        if (!isFading)
        {
            StartCoroutine(FadeRoutine(onFadeComplete));
        }
    }

    // Coroutine für den Fade-Prozess
    private IEnumerator FadeRoutine(System.Action onFadeComplete)
    {
        isFading = true;

        playerMovement.ToggleMovement(true);
        // Fade-Out (in Schwarz)
        yield return StartCoroutine(Fade(1f, unlockMovementDuringFadeOut));

        // Eventual Kamera bewegen oder andere Aktionen durchführen
        onFadeComplete?.Invoke();

        // Fade-In (von Schwarz)
        yield return StartCoroutine(Fade(0f, unlockMovementDuringFadeIn));
        playerMovement.ToggleMovement(false);

        isFading = false;
    }

    // Methode zum eigentlichen Faden
    private IEnumerator Fade(float targetAlpha, float unlockAt)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0f;
        bool movementUnlocked = false;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            if (progress >= unlockAt && !movementUnlocked)
            {
                playerMovement.movementDisabled = false;
                movementUnlocked = true;
            }
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        // Bewegung zum festgelegten Zeitpunkt entsperren
     
    }
}