using UnityEngine;
public class OrangeHeatBehavior : MonoBehaviour, IProjectileBehavior {
    public void Initialize(WandController wand) { }

    void OnCollisionEnter(Collision collision) {
        IHeatable heatableObject = collision.gameObject.GetComponent<IHeatable>();
        
        if (heatableObject != null) heatableObject.ApplyHeat();
        FireballPoolManager.Instance.ReturnFireball(this.gameObject);
    }
}