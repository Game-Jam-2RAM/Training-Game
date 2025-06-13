using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 5.0f;
    public float zoomSpeed = 2.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 10.0f;

    [Header("Rotation Limits")]
    public float minVerticalAngle = -60.0f;
    public float maxVerticalAngle = 120.0f;

    private float currentYaw;
    private float currentPitch = 20.0f;

    void Start()
    {
        if (target != null)
        {
            currentYaw = target.eulerAngles.y; // Start behind the player
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Rotate only when Right Mouse Button is held down
        if (Input.GetMouseButton(1)) // 1 = Right Mouse Button
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentYaw += mouseX;
            currentPitch -= mouseY;
            currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);
        }

        // Zoom Input (always enabled)
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance -= scroll;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

        // Calculate position
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Apply to camera
        transform.position = targetPosition;
        transform.LookAt(target.position + Vector3.up * height * 0.5f);
    }
}
