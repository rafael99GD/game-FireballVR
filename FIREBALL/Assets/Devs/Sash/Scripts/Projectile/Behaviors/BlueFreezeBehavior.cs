using UnityEngine;

public class BlueFreezeBehavior : MonoBehaviour, IProjectileBehavior {

    public void Initialize(WandController wand) { }

    void OnCollisionEnter(Collision collision) {
        IFreezable freezableObject = collision.gameObject.GetComponent<IFreezable>();
        
        if (freezableObject != null) freezableObject.ApplyFreeze();
        FireballPoolManager.Instance.ReturnFireball(this.gameObject);
    }
}