using UnityEngine;

public class SimpleActivator : MonoBehaviour, IActivatable
{
    public GameObject objectToDisable; 
    public ParticleSystem activationParticles; 
    
    public void OnActivate()
    {
        if (activationParticles != null) {
            Instantiate(activationParticles, transform.position, Quaternion.identity);
        }

        if (objectToDisable != null) objectToDisable.SetActive(false); 
        DisableSelf();
    }

    private void DisableSelf() {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        MeshRenderer rend = GetComponent<MeshRenderer>();
        if (rend != null) rend.material.color = Color.gray;
    }
}