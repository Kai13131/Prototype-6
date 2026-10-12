using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    public Camera camera;
    public float MouseSensitivity = 5f;
    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CameraMove();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z + transform.right * x;
        move = move.normalized * speed;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }

    void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * MouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(0f, mouseX, 0f);

        // Rotate the camera vertically
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
