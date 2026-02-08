using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadePanel;      // full-screen black Image
    [SerializeField] private TMP_Text messageText; // your thank-you text
    [SerializeField] private float fadeSeconds = 1.0f;
    [SerializeField] private float messageDelay = 0.3f;

    private bool shown = false;

    private void Start()
    {
        SetAlpha(fadePanel, 0f);
        SetAlpha(messageText, 0f);
    }

    public void ShowEndScreen()
    {
        if (shown) return;
        shown = true;

        // freeze gameplay (optional)
        Time.timeScale = 0f;

        // fade using unscaled time so it still fades while timeScale=0
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        float t = 0f;
        while (t < fadeSeconds)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(t / fadeSeconds);
            SetAlpha(fadePanel, a * 0.75f); // dim, not full black
            yield return null;
        }

        yield return new WaitForSecondsRealtime(messageDelay);

        // fade text in
        t = 0f;
        while (t < 0.5f)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(t / 0.5f);
            SetAlpha(messageText, a);
            yield return null;
        }
    }

    private void SetAlpha(Image img, float a)
    {
        if (!img) return;
        var c = img.color; c.a = a; img.color = c;
    }

    private void SetAlpha(TMP_Text txt, float a)
    {
        if (!txt) return;
        var c = txt.color; c.a = a; txt.color = c;
    }
}
