using UnityEngine;

public class JarDestroy : MonoBehaviour
{
    [Header("Settings")]
    public GameObject particlePrefab;
    public LayerMask targetLayer; // Set this to your 'Fireball' layer in Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // specific check: only triggers if the object is in the target layer
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            collision.gameObject.SetActive(false); // Deactivate the fireball
            BreakJar();
        }
    }

    private void BreakJar()
    {
        if (transform.parent != null)
        {
            JarManager manager = transform.parent.GetComponent<JarManager>();
            if (manager != null)
            {
                manager.JarDestroyed();
            }
        }

        // Instantiate explosion with offset
        GameObject debris = Instantiate(particlePrefab, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);

        // Destroy the jar
        Destroy(gameObject);

        // Cleanup debris
        Destroy(debris, 5.0f);
    }
}
