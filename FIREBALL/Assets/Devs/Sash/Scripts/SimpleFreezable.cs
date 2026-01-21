using UnityEngine;

public class SimpleFreezable : MonoBehaviour, IFreezable {
    public Color frozenColor = Color.blue; 
    private MeshRenderer meshRenderer;
    private bool isFrozen = false;

    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();        
    }

    public void ApplyFreeze() {
        if (isFrozen || meshRenderer == null) return;
        isFrozen = true;

        meshRenderer.material.color = frozenColor;
    }
}