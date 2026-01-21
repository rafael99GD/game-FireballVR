using UnityEngine;

[RequireComponent(typeof(Light))]
public class Torch : MonoBehaviour
{
    private Light _light;
    private bool _isLighting = false;

    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float fadeSpeed = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _light = GetComponent<Light>();
        if (_light != null)
        {
            _light.enabled = true;
            _light.intensity = 0f;
        }
    }

    void Update()
    {
        if(_isLighting && _light.intensity < maxIntensity)
        {
            _light.intensity += Time.deltaTime * fadeSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TORCH collider detectado: " + other.gameObject.name);
        
        IProjectileBehavior gem = other.GetComponent<IProjectileBehavior>();

        if(gem != null && _light != null)
        {
            _isLighting = true;
        }
    }
}
