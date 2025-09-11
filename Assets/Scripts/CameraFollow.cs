using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    private float startPOV, targetFOV;

    public float zoomSpeed = 1f;

    public Camera theCam;

    public float rotationSpeed = 5.0f;
    private float mouseX, mouseY;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        startPOV = theCam.fieldOfView;
        targetFOV = startPOV;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        CameraRotate();
        transform.position = target.position +new Vector3 (0,2,0);
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        //
    }

    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }
    public void ZoomOut()
    {
        targetFOV = startPOV;

    }
    private void CameraRotate()
    {
        // Get mouse input
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Clamp vertical rotation to prevent flipping
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        // Rotate the camera based on mouse input
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        // Optional: If orbiting around a target, update position accordingly
        if (target.transform.position != Vector3.zero)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            Vector3 offset = new Vector3(0, 0, -distance);
            transform.position = target.position + transform.rotation * offset;
        }
    }


}



