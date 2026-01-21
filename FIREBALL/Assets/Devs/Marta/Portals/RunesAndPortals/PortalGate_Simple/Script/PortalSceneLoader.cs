using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneLoader : MonoBehaviour
{
    [Header("Arrastra aquí la escena destino (Editor)")]
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset sceneAsset;
#endif

    [Header("Nombre de escena (se autocompleta al arrastrar arriba)")]
    [SerializeField] private string sceneName;

    [Header("Player root (XR Origin / OVRCameraRig)")]
    [SerializeField] private Transform playerRoot;

    [Header("Evitar dobles triggers")]
    [SerializeField] private float triggerCooldownSeconds = 0.2f;

    private bool loading;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }
#endif

    private void Reset()
    {
        // Para triggers fiables en VR: Rigidbody kinematic en el trigger
        var rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        // Asegura collider trigger
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (loading) return;

        bool isPlayer = playerRoot != null
            ? other.transform.IsChildOf(playerRoot)
            : other.CompareTag("Player");

        if (!isPlayer) return;

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("PortalSceneLoader: No hay escena asignada. Arrastra una escena al campo 'Scene Asset'.");
            return;
        }

        // Comprueba que esté en Build Profile / Scenes In Build
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError(
                $"PortalSceneLoader: La escena '{sceneName}' no se puede cargar. " +
                $"Añádela en File -> Build Profiles (o Build Settings -> Scenes In Build)."
            );
            return;
        }

        loading = true;

        if (triggerCooldownSeconds > 0f)
            Invoke(nameof(BeginLoad), triggerCooldownSeconds);
        else
            BeginLoad();
    }

    private void BeginLoad()
    {
        if (ScreenFader.Instance != null)
        {
            ScreenFader.Instance.FadeOutThen(() =>
            {
                SceneManager.LoadScene(sceneName);
            });
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
