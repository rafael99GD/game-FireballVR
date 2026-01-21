using UnityEngine;

public class GreenBehavior : MonoBehaviour, IProjectileBehavior
{
    private GameObject platformPrefab;
    private bool hasSpawned = false;

    public void Initialize(WandController wand)
    {
        this.platformPrefab = wand.greenPlatformPrefab;
        hasSpawned = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasSpawned) return;

        if (collision.gameObject.GetComponent<EnemyManager>() != null || collision.gameObject.CompareTag("Player"))
        {
            ReturnToPool();
            return;
        }

        if (platformPrefab != null)
        {
            SpawnPlatform(collision);
            hasSpawned = true;
        }

        ReturnToPool();
    }

    private void SpawnPlatform(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 spawnPosition = contact.point;

        Quaternion spawnRotation = Quaternion.LookRotation(contact.normal);
        Instantiate(platformPrefab, spawnPosition, spawnRotation);
    }

    private void ReturnToPool()
    {
        FireballPoolManager.Instance.ReturnFireball(this.gameObject);
    }
}