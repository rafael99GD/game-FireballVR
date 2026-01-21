using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Tooltip("Transform (posición y rotación) a donde se teletransportará el jugador si cae al agua.")]
    public Transform respawnPoint;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        // 1. Lógica de Teletransporte del Jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador cayó a los pinchos. Ejecutando Respawn.");
            // A veces el collider es la mano o el pie, buscamos la raíz (el XR Rig completo)
            TeleportPlayer(other.transform.root.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (respawnPoint == null)
        {
            Debug.LogError("¡ERROR! Los pinchos no tienen asignado el punto de respawn.");
            return;
        }

        // Desactivamos el CharacterController momentáneamente si el jugador lo tiene,
        // ya que a veces impide el teletransporte directo.
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        if (cc != null) cc.enabled = true;

        Debug.Log($"Jugador teletransportado a {respawnPoint.position}.");
    }
}
