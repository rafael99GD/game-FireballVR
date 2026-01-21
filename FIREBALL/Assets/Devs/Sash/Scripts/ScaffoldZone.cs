using UnityEngine;
using System.Collections.Generic;

public class ScaffoldZone : MonoBehaviour
{
    public float lifeTime = 60.0f;
    public float climbSpeed = 3.0f;
    public float enemySlowFactor = 0.4f;

    private Rigidbody playerRb;
    private List<EnemyAi> affectedEnemies = new List<EnemyAi>();

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"{other.name}");
        EnemyAi enemy = other.GetComponent<EnemyAi>();
        if (enemy != null)
        {
            if (!affectedEnemies.Contains(enemy)) {
                enemy.SetSpeedModifier(enemySlowFactor);
                affectedEnemies.Add(enemy);
            }
        }

        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.useGravity = false; 
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); 
                playerRb = rb;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) other.transform.Translate(Vector3.up * climbSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyAi enemy = other.GetComponent<EnemyAi>();
        if (enemy != null) {
            enemy.ResetSpeedModifier();
            affectedEnemies.Remove(enemy);
        }

        if (other.CompareTag("Player")) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) RestorePlayerGravity(rb);
        }
    }

    void OnDestroy()
    {
        if (playerRb != null) RestorePlayerGravity(playerRb);
        foreach (var enemy in affectedEnemies) if (enemy != null) enemy.ResetSpeedModifier();
    }

    private void RestorePlayerGravity(Rigidbody rb)
    {
        rb.useGravity = true;
        if (playerRb == rb) playerRb = null;
    }
}