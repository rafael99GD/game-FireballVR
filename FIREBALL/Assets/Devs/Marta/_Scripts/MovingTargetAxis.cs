using UnityEngine;
using System.Collections;

public class MovingTargetAxis : MonoBehaviour
{
    public enum MoveAxis { X, Z }

    [Header("Movimiento")]
    public MoveAxis axis = MoveAxis.X; // Eje de movimiento (X = lado a lado, Z = delante/atrás)
    public float moveRange = 2f;       // Distancia total de movimiento
    public float moveSpeed = 2f;       // Velocidad de movimiento

    [Header("Respawn")]
    public float disableTime = 5f;     // Tiempo desactivado tras ser golpeado

    private Vector3 startPos;
    private bool isActive = true;

    private Collider[] colliders;
    private Renderer[] renderers;

    void Start()
    {
        startPos = transform.position;

        colliders = GetComponentsInChildren<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (!isActive) return;

        float offset = Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2f);

        // Según el eje elegido, movemos en X o en Z
        if (axis == MoveAxis.X)
        {
            transform.position = new Vector3(
                startPos.x + offset,
                startPos.y,
                startPos.z
            );
        }
        else // MoveAxis.Z
        {
            transform.position = new Vector3(
                startPos.x,
                startPos.y,
                startPos.z + offset
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Fireball"))
        {
            StartCoroutine(DisableTemporarily());
        }
    }

    private IEnumerator DisableTemporarily()
    {
        isActive = false;

        foreach (var r in renderers)
            r.enabled = false;

        foreach (var c in colliders)
            c.enabled = false;

        yield return new WaitForSeconds(disableTime);

        foreach (var r in renderers)
            r.enabled = true;

        foreach (var c in colliders)
            c.enabled = true;

        isActive = true;
    }
}
