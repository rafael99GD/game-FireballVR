using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [Header("Configuración del Puente")]
    [Tooltip("Prefab de la plataforma de hielo (Debe tener el script IcePlatform)")]
    public GameObject plataformaHieloPrefab;

    [Tooltip("Duración del puente en segundos")]
    public float duracionPuente = 15f;

    [Tooltip("Altura sobre el agua")]
    public float alturaPlataforma = 0.15f;

    [Header("Configuración de Seguridad")]
    [Tooltip("Transform (posición y rotación) a donde se teletransportará el jugador si cae al agua.")]
    public Transform respawnPoint;

    // CAMBIO IMPORTANTE: Usamos OnTriggerEnter en lugar de OnCollisionEnter
    // Esto permite que el jugador atraviese el agua (efecto hundirse)
    void OnTriggerEnter(Collider other)
    {
        // 1. Lógica de Teletransporte del Jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador cayó al agua. Ejecutando Respawn.");
            // A veces el collider es la mano o el pie, buscamos la raíz (el XR Rig completo)
            TeleportPlayer(other.transform.root.gameObject);
        }

        // 2. Lógica de ataque de congelación (Bola de la varita)
        // Intentamos obtener el script de la bola
        BlueFreezeBehavior freezeAttack = other.GetComponent<BlueFreezeBehavior>();

        if (freezeAttack != null)
        {
            // En un Trigger no hay "punto de contacto" exacto porque se atraviesan.
            // Usamos la posición de la bola (other.transform.position) como referencia.
            Vector3 puntoAproximado = other.transform.position;

            CrearPlataformaHielo(puntoAproximado);

            // Opcional: Destruir la bola para que no siga cayendo al infinito
            Destroy(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (respawnPoint == null)
        {
            Debug.LogError("¡ERROR! La zona de agua no tiene asignado el punto de respawn.");
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

    void CrearPlataformaHielo(Vector3 posicionBola)
    {
        // Usamos la X y Z de la bola, pero forzamos la Y (altura) a la del agua + offset
        Vector3 posicionPlataforma = new Vector3(
            posicionBola.x,
            transform.position.y + alturaPlataforma, // Usamos la altura del objeto Agua
            posicionBola.z
        );

        GameObject nuevaPlataforma = Instantiate(plataformaHieloPrefab, posicionPlataforma, Quaternion.identity);

        IcePlatform scriptPlataforma = nuevaPlataforma.GetComponent<IcePlatform>();
        if (scriptPlataforma != null) scriptPlataforma.Initialize(duracionPuente);
        else Debug.LogWarning("El prefab de hielo no tiene el script IcePlatform asignado.");
    }
}