using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class WandSocket : MonoBehaviour
{
    [Header("References")]
    public WandController wandController;
    public MeshFilter wandMeshFilter;
    private XRSocketInteractor socketInteractor;

    [Header("Visual Assets")]
    public Mesh defaultMesh;
    public Mesh blueMesh;
    public Mesh redMesh;
    public Mesh greenMesh;
    public Mesh purpleMesh;
    public Mesh yellowMesh;

    void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        
        if (wandController == null) wandController = GetComponentInParent<WandController>();
        if (wandMeshFilter == null) Debug.LogWarning("WandSocket: MeshFilter is not assigned!");
    }

    void OnEnable()
    {
        if (socketInteractor != null) {
            socketInteractor.selectEntered.AddListener(OnGemInserted);
            socketInteractor.selectExited.AddListener(OnGemRemoved);
        }

        if (wandController != null) wandController.onBehaviorChanged += OnAmmoChanged;
    }

    void OnDisable()
    {
        if (socketInteractor != null) {
            socketInteractor.selectEntered.RemoveListener(OnGemInserted);
            socketInteractor.selectExited.RemoveListener(OnGemRemoved);
        }

        if (wandController != null) wandController.onBehaviorChanged -= OnAmmoChanged;
    }

    private void OnGemInserted(SelectEnterEventArgs args)
    {
        GameObject gemObj = args.interactableObject.transform.gameObject;
        IGem gemData = gemObj.GetComponent<IGem>();

        if (gemData != null && wandController != null) {
            wandController.LoadSpecialShots(gemData);
            UpdateWandMesh(gemObj.name.ToLower());
        }
    }

    private void OnGemRemoved(SelectExitEventArgs args)
    {
        ResetMeshToDefault();
    }

    private void OnAmmoChanged(System.Type behaviorType, int shotsRemaining)
    {
        if (shotsRemaining <= 0){
            if (socketInteractor.hasSelection) {
                var attachedGem = socketInteractor.interactablesSelected[0].transform.gameObject;
                Destroy(attachedGem);
            }
            else ResetMeshToDefault();
        }
    }

    private void ResetMeshToDefault()
    {
        if (wandMeshFilter != null && defaultMesh != null) wandMeshFilter.mesh = defaultMesh;
    }

    private void UpdateWandMesh(string gemName)
    {
        if (wandMeshFilter == null) return;

        Mesh targetMesh = null;

        if (gemName.Contains("blue")) targetMesh = blueMesh;
        else if (gemName.Contains("red")) targetMesh = redMesh;
        else if (gemName.Contains("green")) targetMesh = greenMesh;
        else if (gemName.Contains("purple")) targetMesh = purpleMesh;
        else if (gemName.Contains("yellow")) targetMesh = yellowMesh;

        if (targetMesh != null) wandMeshFilter.mesh = targetMesh;
    }
}