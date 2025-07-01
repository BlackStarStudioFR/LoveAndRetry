using UnityEngine;

public class PlayerKillAndRespawn : MonoBehaviour
{
    private Camera mainCam;
    public Transform respawnPoint; // à définir dans l'inspecteur
    private Vector3 initialPosition;

    void Start()
    {
        mainCam = Camera.main;
        if (respawnPoint == null)
        {
            // Si aucun point défini, on prend la position actuelle
            initialPosition = transform.position;
        }
        else
        {
            initialPosition = respawnPoint.position;
        }
    }

    void Update()
    {
        Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);

        // Si hors caméra (marge légère possible)
        if (viewPos.x < 0 || viewPos.x > 1 ||
            viewPos.y < 0 || viewPos.y > 1 ||
            viewPos.z < 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("💀 Mort ! Respawn...");

        // Replace le joueur
        transform.position = initialPosition;

        // Stoppe son mouvement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Reset la caméra
        TowerCameraLock camLock = FindObjectOfType<TowerCameraLock>();
        if (camLock != null)
            camLock.ResetMaxHeight(initialPosition.y);
    }
}
