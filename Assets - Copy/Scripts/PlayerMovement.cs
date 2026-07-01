using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    [Header("References")]
    public Transform playerCamera;

    private Rigidbody rb;
    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Mengunci dan menyembunyikan kursor mouse di tengah layar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotasi Horizontal: Memutar badan player ke kiri/kanan
        transform.Rotate(0f, mouseX, 0f);

        // Rotasi Vertikal: Memutar kamera saja ke atas/bawah
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // Tombol A/D atau Arrow kiri/kanan
        float v = Input.GetAxis("Vertical");   // Tombol W/S atau Arrow atas/bawah

        Vector3 moveDir = transform.right * h + transform.forward * v;
        moveDir = moveDir.normalized * moveSpeed;

        // Mendukung kompabilitas Unity versi lama maupun baru untuk kalkulasi velocity
#if UNITY_2023_1_OR_NEWER
        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
#else
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
#endif
    }
}