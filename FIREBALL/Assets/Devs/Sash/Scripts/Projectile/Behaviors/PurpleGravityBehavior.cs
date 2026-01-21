using UnityEngine;

public class PurpleGravityBehavior : MonoBehaviour, IProjectileBehavior
{
    private GameObject singularityPrefab;
    private bool hasActivated = false;

    public void Initialize(WandController wand)
    {
        this.singularityPrefab = wand.purpleBlackHolePrefab;
        hasActivated = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasActivated) return;
        if (collision.gameObject.CompareTag("Player")) return;

        SpawnSingularity(collision);
        hasActivated = true;

        FireballPoolManager.Instance.ReturnFireball(this.gameObject);
    }

    private void SpawnSingularity(Collision collision)
    {
        if (singularityPrefab == null) return;
        ContactPoint contact = collision.contacts[0];
        Vector3 spawnPos = contact.point + (contact.normal * 0.5f);
        
        Instantiate(singularityPrefab, spawnPos, Quaternion.identity);
    }
}