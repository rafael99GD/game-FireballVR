using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class RedBounceBehavior : MonoBehaviour, IProjectileBehavior {
    
    private string targetTag;
    public int maxBounces = 10;
    private int currentBounces = 0;
    
    private Rigidbody rb;
    private float constantSpeed; 

    public void Initialize(WandController wand) {
        rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();

        // 1. FÍSICA ARCADE
        rb.useGravity = false; 
        rb.linearDamping = 0; 
        rb.angularDamping = 0;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        // rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; // Opcional

        // 2. Material de rebote
        PhysicsMaterial mat = new PhysicsMaterial();
        mat.bounciness = 1f;       
        mat.dynamicFriction = 0f;  
        mat.staticFriction = 0f;
        mat.frictionCombine = PhysicsMaterialCombine.Minimum;
        mat.bounceCombine = PhysicsMaterialCombine.Maximum;
        col.material = mat;

        this.targetTag = wand.redBallTargetTag;
        this.constantSpeed = wand.launchForce; 
        currentBounces = 0;
    }

    void FixedUpdate() {
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0) {
            rb.linearVelocity = rb.linearVelocity.normalized * constantSpeed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.CompareTag(targetTag)) {
            IActivatable activatable = collision.gameObject.GetComponent<IActivatable>();
            
            if (activatable != null) {
                activatable.OnActivate();
            } else {
                Destroy(collision.gameObject);
            }

            ReturnToPool();
            return;
        }

        currentBounces++;
        
        if (currentBounces >= maxBounces) {
            ReturnToPool();
        } else {
            if (rb.linearVelocity != Vector3.zero) transform.forward = rb.linearVelocity;
        }
    }

    private void ReturnToPool() {
        rb.useGravity = true; 
        rb.constraints = RigidbodyConstraints.None;
        FireballPoolManager.Instance.ReturnFireball(this.gameObject);
    }
}