using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 9f;
    public float verticalSpeed = 4f;
    public float mouseSensitivity = 3f;

    float rotationX = 0f;

    void Update()
    {
        // ===== MOUSE LOOK =====
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // ===== GERAK WASD =====
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        Vector3 move = (transform.right * x + transform.forward * z) * speed;

        // ===== NAIK TURUN =====
        if (Input.GetKey(KeyCode.Space))
            move.y += verticalSpeed;

        if (Input.GetKey(KeyCode.LeftControl))
            move.y -= verticalSpeed;

        transform.position += move * Time.deltaTime;
    }
}