using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PhotoController : MonoBehaviour
{   

    public bool photoMode = false;
    public bool journalMode = false;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject journalUI;

    [SerializeField] private CameraController controller;
    [SerializeField] private GameObject controlUI;
    private GameObject normalControls;
    private GameObject cameraControls;
    private bool controlSwitch = true; // true for normal, false for camera

    [Header("Camera Zoom")]
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 60f;
    
    private Camera cam;
    
    void Start()
    {   
        // get the player's camera
        cam = gameObject.GetComponentInChildren<Camera>();
        normalControls = GameObject.FindGameObjectWithTag("NormalControls");
        normalControls.SetActive(true);
        cameraControls = GameObject.FindGameObjectWithTag("CameraControls");
        cameraControls.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && !journalMode) {
            photoMode = !photoMode;
            cameraUI.SetActive(photoMode);
            controlSwitch = !controlSwitch;
            normalControls.SetActive(controlSwitch);
            cameraControls.SetActive(!controlSwitch);
        }

        if (Input.GetKeyDown(KeyCode.B) && !photoMode) {
            journalMode = !journalMode;
            journalUI.SetActive(journalMode);
            controlSwitch = !controlSwitch;
            normalControls.SetActive(controlSwitch);
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (journalMode)
        {
            controller.enabled = false; // turn off fps camera controller
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            controller.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }  

        // zooming in and out 
        if (photoMode) {
            if (cam) {
                cam.fieldOfView -= scrollInput * zoomSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
            }
        }
        else 
        {
            cam.fieldOfView = 60f;
        }
    }
}
