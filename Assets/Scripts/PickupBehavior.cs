using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public float interactDistance = 3f;
    public float interactAngle = 30f; // Field of view tolerance (in degrees)
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.F;

    private GameObject currentTarget;
    private Camera mainCamera;
    private string[] itemList = {"Chips", "Soda", "Milk", "Ice Cream", "Air Freshener", "Toilet Paper", "Bananas", "Apples", "Chocolate", "Magazine"};

    void Start()
    {
        mainCamera = Camera.main;

    }

    void Update()
    {
        Collider[] nearbyItems = Physics.OverlapSphere(transform.position, interactDistance, interactableLayer);
        GameObject bestCandidate = null;
        float bestAngle = interactAngle;

        foreach (Collider col in nearbyItems)
        {
            Vector3 toItem = col.transform.position - mainCamera.transform.position;
            float angle = Vector3.Angle(mainCamera.transform.forward, toItem);

            if (angle < bestAngle)
            {
                // Optional: confirm it's an interactable item
                if (col.GetComponent<ItemBehavior>() != null)
                {
                    bestAngle = angle;
                    bestCandidate = col.gameObject;
                }
            }
        }

        if (bestCandidate != null)
        {
            if (currentTarget != bestCandidate)
            {
                currentTarget = bestCandidate;
                ShowPrompt(true, currentTarget.name);
            }

            if (Input.GetKeyDown(interactKey))
            {   
                if (currentTarget.name ==  )
                PickUpItem(currentTarget);
            }
        }
        else if (currentTarget != null)
        {
            ShowPrompt(false);
            currentTarget = null;
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
            Debug.Log($"[F] to pick up {itemName}");
        }
        else
        {
            Debug.Log(" ");
        }
    }
}
