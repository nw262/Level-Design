using UnityEditor.Rendering;
using UnityEngine;

public class PhotoController : MonoBehaviour
{   

    public bool photoMode = false;
    [SerializeField] GameObject cameraUI;

    [Header("Camera Zoom")]
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 60f;
    
    private Camera cam;
    
    void Start()
    {   
        // get the player's camera
        cam = gameObject.GetComponentInChildren<Camera>();
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) {
            photoMode = !photoMode;
            cameraUI.SetActive(photoMode);
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

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
