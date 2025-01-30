using UnityEngine;

// Meant to be added to a Camera component
// Adds the ability for the player to look at something and interact with an "Interactable" object.
public class PlayerInteraction : MonoBehaviour
{      

    public float range = 1f; // the distance the player has to be in order to interact with something

    private Interactable currTarget; // the Interactable target object the player is currently looking at

    // Update is called once per frame
    void Update()
    {   
        HandleRaycast();

        // if player presses 'F' interact with the object
        if (currTarget) {
            if (Input.GetKeyDown(KeyCode.F))
            {
                currTarget.OnInteract();
            }
        }
    }

    // helper function that creates the Raycast and handles the information it provides
    void HandleRaycast() 
    {

        // Create a ray that starts at the camera and points forward
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * 10);

        // raycast has hit an object 
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Interactable hitObject = hit.collider.GetComponent<Interactable>();

            // case 1: look away from current target to a new target
            if (hitObject) {

                // check if we are still looking at the same target
                if (hitObject != currTarget) {
                    if (currTarget) {
                        // run the current targets look away script
                        currTarget.OnLookAway();
                    }
                }

                // update current target to the new one and run its look script
                currTarget = hitObject;
                currTarget.OnLook();
            }
            // case 2: look away from current target but not to a new target
            else if (currTarget) {
                // set current target to null and run the look away script
                currTarget.OnLookAway();
                currTarget = null;
            }
        }
        // raycast has not hit anything
        else if (currTarget)
        {
            // set current target to null and run the look away script
            currTarget.OnLookAway();
            currTarget = null;
        }
    }
}
