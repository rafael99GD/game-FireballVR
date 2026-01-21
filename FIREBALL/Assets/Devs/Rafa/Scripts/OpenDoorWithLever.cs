using UnityEngine;

public class OpenDoorWithLever : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Transform doorOpenPosition;
    [SerializeField] private Transform doorClosePosition;

    public void onLeverOn()
    {
        door.transform.position = doorOpenPosition.position;
        door.transform.rotation = doorOpenPosition.rotation;
    }

    public void onLeverOff()
    {
        door.transform.position = doorClosePosition.position;
        door.transform.rotation = doorClosePosition.rotation;
    }
}
