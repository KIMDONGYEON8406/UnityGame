using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 🚀 씬 전환 후에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // 🚀 게임 시작 시 FadeIn 실행
    }

    public void StartFadeOutThen(Action onFadeComplete)
    {
        if (fadeImage == null)
        {
            Debug.LogError("❌ FadeManager: fadeImage가 설정되지 않음! Inspector에서 설정 필요!");
            onFadeComplete?.Invoke();
            return;
        }

        StartCoroutine(FadeOutThenAction(onFadeComplete));
    }

    public void StartFadeIn()
    {
        if (fadeImage == null)
        {
            Debug.LogError("❌ FadeManager: fadeImage가 설정되지 않음!");
            return;
        }

        StartCoroutine(FadeIn()); // 🚀 FadeIn 실행
    }

    private IEnumerator FadeIn()
    {
        float alpha = 1;
        fadeImage.gameObject.SetActive(true); // 🚀 Fade 시작 전 활성화
        fadeImage.raycastTarget = true; // 🚀 클릭 방지

        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.raycastTarget = false; // 🚀 FadeIn 완료 후 UI 클릭 가능
        fadeImage.gameObject.SetActive(false); // 🚀 FadeIn이 끝나면 비활성화
    }

    private IEnumerator FadeOutThenAction(Action onFadeComplete)
    {
        float alpha = 0;
        fadeImage.gameObject.SetActive(true); // 🚀 FadeOut 시작 전 활성화
        fadeImage.raycastTarget = true; // 🚀 클릭 방지

        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        Debug.Log("🎬 FadeOut 완료, 이동 실행!");
        onFadeComplete?.Invoke(); // 🚀 이동 실행

        yield return new WaitForSeconds(0.1f); // 🚀 이동 후 잠깐 대기

        StartCoroutine(FadeIn()); // 🚀 이동 후 FadeIn 실행
    }
}




