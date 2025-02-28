using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; // 페이드 효과를 적용할 CanvasGroup
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // 시작할 때 알파 값을 0으로 설정 (화면이 투명)
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
        // 1. 화면 어둡게 (페이드 아웃)
        yield return StartCoroutine(Fade(1));

        // 2. 씬 전환 등의 동작 실행
        onFadeComplete?.Invoke();

        // 3. 잠시 대기
        yield return new WaitForSeconds(0.2f);

        // 4. 화면 밝게 (페이드 인)
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

