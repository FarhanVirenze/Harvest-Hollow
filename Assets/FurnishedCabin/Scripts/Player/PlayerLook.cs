using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public float mouseSensitivity = 120f;
    public float distance = 6f;
    public float height = 3.5f;

    public float minY = -20f;
    public float maxY = 60f;

    private float yaw;
    private float pitch;

    void LateUpdate()
    {
        if (!target) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation * new Vector3(0, height, -distance);
        transform.position = target.position + offset;

        transform.LookAt(target.position + Vector3.up * height);
    }
}
