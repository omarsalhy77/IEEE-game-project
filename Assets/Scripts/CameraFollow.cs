using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    private float targetFOV;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

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
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //Debug.Log($"Mouse X: {mouseX}, Mouse Y: {mouseY}");
        transform.position = target.position +new Vector3 (0,2,0);
        transform.rotation = target.rotation;

        //theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        MouseInput();
    }

    private void MouseInput()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera vertically and clamp rotation to prevent flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotate player body horizontally
        
            target.Rotate(Vector3.up * mouseX);
        
    }


}



