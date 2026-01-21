using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WandColorChanger : MonoBehaviour
{
    [Header("Wand Mesh Settings")]
    [SerializeField] private MeshFilter wandMeshFilter;
    [SerializeField] private MeshRenderer wandMeshRenderer;
    
    [Header("Wand Meshes by Color")]
    [SerializeField] private Mesh blueMesh;
    [SerializeField] private Mesh greenMesh;
    [SerializeField] private Mesh purpleMesh;
    [SerializeField] private Mesh redMesh;
    [SerializeField] private Mesh yellowMesh;
    
    [Header("Socket Interactor")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;
    
    private void Start()
    {
        if (wandMeshFilter == null)
        {
            wandMeshFilter = GetComponentInChildren<MeshFilter>();
            if (wandMeshFilter == null)
            {
                Debug.LogError("WandColorChanger: No se encontró MeshFilter en la varita");
                return;
            }
        }
        
        if (wandMeshRenderer == null)
        {
            wandMeshRenderer = GetComponentInChildren<MeshRenderer>();
        }
        
        if (socketInteractor == null)
        {
            socketInteractor = GetComponentInChildren<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
            if (socketInteractor == null)
            {
                Debug.LogError("WandColorChanger: No se encontró XRSocketInteractor");
                return;
            }
        }
        
        socketInteractor.selectEntered.AddListener(OnGemInserted);
        socketInteractor.selectExited.AddListener(OnGemRemoved);
    }
    
    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnGemInserted);
            socketInteractor.selectExited.RemoveListener(OnGemRemoved);
        }
    }
    
    private void OnGemInserted(SelectEnterEventArgs args)
    {
        GameObject gem = args.interactableObject.transform.gameObject;
        
        // Detectar el color según el nombre del objeto
        string gemName = gem.name.ToLower();
        
        Mesh newMesh = null;
        
        if (gemName.Contains("blue"))
        {
            newMesh = blueMesh;
        }
        else if (gemName.Contains("green"))
        {
            newMesh = greenMesh;
        }
        else if (gemName.Contains("purple"))
        {
            newMesh = purpleMesh;
        }
        else if (gemName.Contains("red"))
        {
            newMesh = redMesh;
        }
        else if (gemName.Contains("yellow"))
        {
            newMesh = yellowMesh;
        }
        else
        {
        }

        if (newMesh != null && wandMeshFilter != null)
        {
            wandMeshFilter.mesh = newMesh;
        }
    }
    
    private void OnGemRemoved(SelectExitEventArgs args)
    {
        Debug.Log("Gema removida del socket");
    }
}