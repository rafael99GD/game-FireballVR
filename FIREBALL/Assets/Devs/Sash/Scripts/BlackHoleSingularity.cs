using UnityEngine;

public class BlackHoleSingularity : MonoBehaviour
{
    public float pullRadius = 10f;
    public float pullForce = 80f;
    public float duration = 4.0f;
    public string targetTag = "PuzzleTarget";
    public float rotationSpeed = 100f;
    private float timer;

    void Start()
    {
        timer = duration;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        float scaleProgress = 0f;
        if (timer > duration * 0.8f) scaleProgress = (duration - timer) / (duration * 0.2f);
        else if (timer < 0.5f) scaleProgress = timer / 0.5f;
        else scaleProgress = 1f;

        transform.localScale = Vector3.one * scaleProgress * 2f;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (timer <= 0) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        AttractObjects();
    }

    private void AttractObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pullRadius);

        foreach (Collider col in colliders) {
            if (col.CompareTag(targetTag)) {
                Rigidbody rb = col.GetComponent<Rigidbody>();

                if (rb != null) {
                    Vector3 directionToCenter = transform.position - col.transform.position;
                    float distance = directionToCenter.magnitude;

                    Vector3 forceDirection = directionToCenter.normalized;
                    float proximityFactor = 1f / (Mathf.Max(distance, 1f)); 
                    rb.AddForce(forceDirection * pullForce * proximityFactor, ForceMode.Acceleration);
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0.5f, 0f, 0.5f, 0.3f);
        Gizmos.DrawSphere(transform.position, pullRadius);
    }
}