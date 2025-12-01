using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool clickToMoveCamera = false;
    public bool canZoom = true;
    public float sensitivity = 5f;
    public Vector2 cameraLimit = new Vector2(-45, 40);

    public float initialHeight = 2f;
    public float initialDistance = 0.5f;

    private float mouseX;
    private float mouseY;

    private Transform player;

    void Awake()
    {
        // safer player lookup
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // force cursor lock if needed
        if (!clickToMoveCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // zoom
        if (canZoom)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
                Camera.main.fieldOfView -= scroll * sensitivity * 2;
        }

        // if you require RMB to move camera
        if (clickToMoveCamera && Input.GetAxisRaw("Fire2") == 0)
            return;

        // rotation
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        // FOLLOW player while keeping rotation
        Vector3 offset = transform.rotation * new Vector3(0, 0, -initialDistance);
        transform.position = player.position + offset + new Vector3(0, initialHeight, 0);
    }
}
