using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Für den Image-Component, der das Fading steuert

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // UI-Image für das Faden
    public float fadeDuration = 1f; // Dauer des Fadings
    private bool isFading = false;

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
        // Fade-Out (in Schwarz)
        yield return StartCoroutine(Fade(1f));

        // Eventual Kamera bewegen oder andere Aktionen durchführen
        onFadeComplete?.Invoke();

        // Fade-In (von Schwarz)
        yield return StartCoroutine(Fade(0f));

        isFading = false;
    }

    // Methode zum eigentlichen Faden
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}