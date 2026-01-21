using UnityEngine;

public class SimpleHeatable : MonoBehaviour, IHeatable {
    public Color heatedColor = Color.red; 
    private MeshRenderer meshRenderer;
    private bool isHeated = false;

    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ApplyHeat() {
        if (isHeated || meshRenderer == null) return;
        isHeated = true;
        
        meshRenderer.material.color = heatedColor;
    }
}