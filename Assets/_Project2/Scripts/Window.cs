using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window : Interactable
{

    public Text prompt; // UI element that shows up when player is capable of interacting
    public Camera playerCamera;
    public float zoomedFOV = 30f;
    public float zoomSpeed = 5f;
    public float zoomDistance = 1;

    private float defaultFOV; // keep track of original view
    private bool zoomedIn = false;

    // Called when the player looks at the object
    public override void OnLook()
    {
        prompt.enabled = true;
    }

    // Called when the player interacts with the object
    public override void OnInteract()
    {   
        // zoom out
        if (zoomedIn) 
        {
            StartCoroutine(ZoomCamera(defaultFOV));
            playerCamera.transform.localPosition -= new Vector3(0, 0, zoomDistance);
            zoomedIn = false;
        }
        else
        {
            StartCoroutine(ZoomCamera(zoomedFOV));
            playerCamera.transform.localPosition += new Vector3(0, 0, zoomDistance);
            zoomedIn = true;
        }
    }

    // Called when the player stops looking at the object
    public override void OnLookAway()
    {
        prompt.enabled = false;

        // If the player looks away while zoomed in, reset the FOV
        if (zoomedIn)
        {
            StartCoroutine(ZoomCamera(defaultFOV));
            playerCamera.transform.localPosition -= new Vector3(0, 0, zoomDistance);
            zoomedIn = false;
        }
    }

    // Update is called once per frame
    void Start()
    {
        prompt.text = "Press 'F' to peak.";
        prompt.enabled = false;
        prompt.color = Color.red;
        defaultFOV = playerCamera.fieldOfView;
    }

    private IEnumerator ZoomCamera(float targetFOV) 
    {
        while (Mathf.Abs(playerCamera.fieldOfView - targetFOV) > 0.1f)
        {
            // use lerp to create the zoom in effect
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // change FOV to given value
        playerCamera.fieldOfView = targetFOV;
    }
}

