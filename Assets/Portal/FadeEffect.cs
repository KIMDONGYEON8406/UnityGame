using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; // ���̵� ȿ���� ������ CanvasGroup
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // ������ �� ���� ���� 0���� ���� (ȭ���� ����)
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0;
        }
    }

    public void StartFadeInOut(System.Action onFadeComplete)
    {
        StartCoroutine(FadeSequence(onFadeComplete));
    }

    private IEnumerator FadeSequence(System.Action onFadeComplete)
    {
        // 1. ȭ�� ��Ӱ� (���̵� �ƿ�)
        yield return StartCoroutine(Fade(1));

        // 2. �� ��ȯ ���� ���� ����
        onFadeComplete?.Invoke();

        // 3. ��� ���
        yield return new WaitForSeconds(0.2f);

        // 4. ȭ�� ��� (���̵� ��)
        yield return StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}

