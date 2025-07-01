using UnityEngine;
using Unity.Cinemachine;

public class TowerCameraLock : MonoBehaviour
{
    public Transform player;
    public float cameraX = 0f; // Position fixe en X pour centrer sur la tour
    private float maxHeight;
    private CinemachineCamera vcam;
    private Vector3 initialCamPosition;
    private Quaternion initialCamRotation;

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
        if (vcam == null || player == null)
        {
            Debug.LogError("Virtual Camera ou Player non assigné !");
            enabled = false;
            return;
        }

        maxHeight = player.position.y;
        initialCamPosition = vcam.transform.position;
        initialCamRotation = vcam.transform.rotation;
    }

    void LateUpdate()
    {
        if (player.position.y > maxHeight)
            maxHeight = player.position.y;

        Vector3 camPos = vcam.transform.position;

        // Caméra centrée en X
        camPos.x = cameraX;

        // Caméra suit jusqu'à la hauteur max
        camPos.y = maxHeight;

        // Plan 2D fixe
        camPos.z = initialCamPosition.z;

        vcam.transform.position = camPos;
        vcam.transform.rotation = initialCamRotation;
    }
    public void ResetMaxHeight(float newHeight)
    {
        maxHeight = newHeight;
    }
}
