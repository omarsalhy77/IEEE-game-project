using UnityEditor.PackageManager.UI;
using UnityEngine;

public class DoorOpenCLose : MonoBehaviour
{

    [SerializeField] private Animator doorAnimation;
    [SerializeField] public Transform doorTransform;
    public bool doorIsOpen = false;
    public static DoorOpenCLose Instance;
    //[SerializeField] private float rotationSpeed;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //doorTransform = gameObject.GetComponent<Transform>();
        doorAnimation = gameObject.GetComponent<Animator>();

        

    }
    
    public void OpenDoor()
    {
        doorIsOpen = true;
        doorAnimation.SetBool("doorIsOpened", doorIsOpen);
        doorAnimation.Play("OpenDoor");
        
        //Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
        //hasInteracted = true;
        //doorTransform.Rotate(0, -90, 0);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



    }
    public void CloseDoor()
    {
        doorIsOpen = false;
        doorAnimation.SetBool("doorIsOpened", doorIsOpen);
        doorAnimation.Play("DoorClose");
        
        
        //hasInteracted = true;
        //doorTransform.Rotate(0, 90, 0);
       // Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
       // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



    }
}
