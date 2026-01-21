using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(2f, 0, 0);  // hacia donde se abre (2 unidades a la derecha)
    public float openTime = 1f;                         // segundos en abrir/cerrar

    private Vector3 closedPos;
    private Vector3 openPos;

    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;
    }

    public void ToggleDoor()
    {
        if (isMoving) return;
        StartCoroutine(MoveDoor(!isOpen));
    }

    private IEnumerator MoveDoor(bool open)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = open ? openPos : closedPos;

        float elapsed = 0f;

        while (elapsed < openTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / openTime);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        isOpen = open;
        isMoving = false;
    }
}