using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public DoorController door;   // referencia a la puerta
    public bool oneUse = false;   // ahora normalmente lo quieres en false (puedes usarlo varias veces)

    private bool used = false;

    void OnTriggerEnter(Collider other)
    {
        if (used && oneUse) return;

        if (other.CompareTag("Fireball"))
        {
            if (door != null)
            {
                door.ToggleDoor();
            }

            if (oneUse)
            {
                used = true;
            }
        }
    }
}