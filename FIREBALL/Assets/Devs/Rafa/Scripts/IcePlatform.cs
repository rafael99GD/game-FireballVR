using System.Collections;
using UnityEngine;

public class IcePlatform : MonoBehaviour, IHeatable
{
    private float lifeTime = 5f;
    private bool isMelting = false;
    private Renderer myRenderer;

    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    public void Initialize(float duration)
    {
        lifeTime = duration;
        StartCoroutine(LifeCycleRoutine());
    }

    public void ApplyHeat()
    {
        if (isMelting) return;
        
        StopAllCoroutines();
        StartCoroutine(MeltRoutine());
    }

    private IEnumerator LifeCycleRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        yield return StartCoroutine(VisualFlashRoutine());

        Destroy(gameObject);
    }

    private IEnumerator MeltRoutine()
    {
        isMelting = true;
        
        if (myRenderer != null) myRenderer.material.color = Color.red; 
        yield return new WaitForSeconds(0.2f);
        
        Destroy(gameObject);
    }

    private IEnumerator VisualFlashRoutine()
    {
        if (myRenderer != null) {
            for (int i = 0; i < 6; i++) {
                myRenderer.enabled = !myRenderer.enabled;
                yield return new WaitForSeconds(0.15f);
            }
            
            myRenderer.enabled = true;
        }
    }
}