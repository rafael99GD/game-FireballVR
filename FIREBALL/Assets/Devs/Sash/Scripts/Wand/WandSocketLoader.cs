using UnityEngine;

[RequireComponent(typeof(Collider))] 
public class WandSocketLoader : MonoBehaviour {
    private WandController wandController;

    void Awake() {
        wandController = GetComponentInParent<WandController>();
        Collider col = GetComponent<Collider>();
        if (!col.isTrigger) col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other){
        IGem gem = other.GetComponent<IGem>();

        if (gem != null && wandController != null) {
            wandController.LoadSpecialShots(gem);
            Destroy(other.gameObject);
        }
    }
}