using UnityEngine;

public class WandChargesCounter : MonoBehaviour
{
    [SerializeField] private Transform counterBar;
    [SerializeField] private PlayerManager playerManager;

    private float fullBarYScale, fullBarYPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullBarYScale = 0.05f;
        fullBarYPos = 0.1222f;
        playerManager.OnManaChanged.AddListener(SetBarSize);
    }

    public void SetBarSize(float percentLost)
    {
        float newHeight = counterBar.transform.localScale.y - (fullBarYScale * percentLost);
        float newPosY = counterBar.transform.localPosition.y - (fullBarYPos * percentLost);

        counterBar.transform.localScale = new Vector3(
            counterBar.transform.localScale.x,
            newHeight,
            counterBar.transform.localScale.z);

        counterBar.transform.localScale = new Vector3(
            counterBar.transform.localPosition.x,
            newPosY,
            counterBar.transform.localPosition.z);
    }
}
