using UnityEditor.PackageManager.UI;
using UnityEngine;

public class DoorOpenCLose : MonoBehaviour
{

    [SerializeField] private Transform doorTransform;
    public bool doorIsOpen = false;
    public static DoorOpenCLose Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        doorTransform = gameObject.GetComponent<Transform>();
        

    }
    private void Update()
    {
        
    }
    
    public void DoorOpen()
    {
      if (doorIsOpen)
        {
            doorIsOpen = false;
            //hasInteracted = true;
            doorTransform.Rotate(0, -90, 0);

        }
        if (!doorIsOpen)
        {
            doorIsOpen = true;
            //hasInteracted = true;

            doorTransform.Rotate(0, 0, 0);
        }
        
    }
    public void CloseDoor()
    {

    }
}
