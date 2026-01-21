using UnityEngine;

public class JarManager : MonoBehaviour
{
    public static JarManager Instance;

    private int jarCount;
    private static int totalJars;

    public AudioSource all_jars_destroyed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalJars = transform.childCount;
    }

    public void JarDestroyed()
    {
        jarCount++;
        if (jarCount >= totalJars)
        {
            all_jars_destroyed.PlayOneShot(all_jars_destroyed.clip);
            Debug.Log("ALL JARS DESTROYED!");
        }
    }
}
