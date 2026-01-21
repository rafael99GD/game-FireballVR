using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.6f;

    [Header("Auto abrir (fade in) al cargar escena")]
    [SerializeField] private bool fadeInOnSceneLoaded = true;

    [Header("Si arrancas una escena directamente, empieza en negro y abre")]
    [SerializeField] private bool startBlackAndFadeIn = true;

    private Coroutine current;
    private bool isFading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (fadeImage == null)
            fadeImage = GetComponentInChildren<Image>(true);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Start()
    {
        if (startBlackAndFadeIn)
        {
            SetAlpha(1f);
            if (fadeInOnSceneLoaded)
                StartFadeIn();
        }
        else
        {
            SetAlpha(0f);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!fadeInOnSceneLoaded || fadeImage == null) return;

        if (current != null) StopCoroutine(current);
        current = StartCoroutine(FadeInAfterLoad());
    }

    private IEnumerator FadeInAfterLoad()
    {
        // Espera 1 frame para que la escena nueva ya esté lista/renderizando
        yield return null;

        // Asegura negro antes de abrir (evita "saltos")
        SetAlpha(1f);

        // Ahora sí: abrir suave
        yield return Fade(1f, 0f);
    }

    // ✅ SOLO FADE OUT y luego ejecuta acción (ej: cargar escena). Se queda en negro.
    public void FadeOutThen(System.Action afterFadeOut)
    {
        if (isFading) return;

        if (current != null) StopCoroutine(current);
        current = StartCoroutine(FadeOutThenRoutine(afterFadeOut));
    }

    private IEnumerator FadeOutThenRoutine(System.Action afterFadeOut)
    {
        isFading = true;
        yield return Fade(0f, 1f);   // cerrar a negro
        afterFadeOut?.Invoke();      // cargar escena aquí (seguimos en negro)
        isFading = false;
    }

    private void StartFadeIn()
    {
        if (isFading) return;

        if (current != null) StopCoroutine(current);
        current = StartCoroutine(Fade(1f, 0f)); // abrir
    }

    private IEnumerator Fade(float from, float to)
    {
        if (fadeImage == null) yield break;

        float t = 0f;
        var c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, to);
    }

    private void SetAlpha(float a)
    {
        if (fadeImage == null) return;
        var c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, a);
    }
}
