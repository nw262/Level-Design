using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public float interactDistance = 2f;
    public float sphereRadius = 0.3f;
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.F;

    private GameObject currentTarget;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // 🔹 Draw the SphereCast line (center ray) in Scene view
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.cyan);

        // Perform the SphereCast
        if (Physics.SphereCast(ray, sphereRadius, out hit, interactDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.GetComponent<ItemBehavior>() == null)
                return;

            if (hitObject != currentTarget)
            {
                currentTarget = hitObject;
                ShowPrompt(true, hitObject.name);
            }

            if (Input.GetKeyDown(interactKey))
            {
                PickUpItem(currentTarget);
            }
        }
        else
        {
            if (currentTarget != null)
            {
                ShowPrompt(false);
                currentTarget = null;
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        Debug.Log("Picked up: " + item.name);
        Destroy(item);
        ShowPrompt(false);
    }

    void ShowPrompt(bool show, string itemName = "")
    {
        if (show)
        {
            Debug.Log($"[E] to pick up {itemName}");
        }
        else
        {
            Debug.Log(" ");
        }
    }
}
