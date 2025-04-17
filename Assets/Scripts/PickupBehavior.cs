using cakeslice;
using TMPro;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public float interactDistance = 3f;
    public float interactAngle = 30f; // Field of view tolerance (in degrees)
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.F;
    public GameObject pickupItems;
    public TMP_Text pickupPrompt;

    private GameObject currentTarget;
    private Camera mainCamera;
    private GameObject[] itemList;
    private int currentItem = 0;
    private bool interact;

    void Start()
    {
        mainCamera = Camera.main;

        itemList = new GameObject[pickupItems.transform.childCount];

        for (int i = 0; i < pickupItems.transform.childCount; i++)
        {   
            if (pickupItems.transform.GetChild(i).tag == "ToiletPaper")
            {
                itemList[i] = pickupItems.transform.GetChild(i).GetChild(0).gameObject;
            }
            else
                itemList[i] = pickupItems.transform.GetChild(i).gameObject;
        }

        itemList[0].GetComponent<Outline>().eraseRenderer = false;
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

                if (col.GetComponent<CashierBehavior>() != null && currentItem == itemList.Length)
                {
                    interact = true;
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
                if (interact)
                    ShowPrompt(true, "Cashier");
                else if (currentTarget.name == itemList[currentItem].name)
                    ShowPrompt(true, currentTarget.name);
            }

            if (Input.GetKeyDown(interactKey))
            {
                if (interact)
                    currentTarget.GetComponent<CashierBehavior>().TurnAround();
                else if (currentTarget.name == itemList[currentItem].name)
                {
                    PickUpItem(currentTarget);
                    currentItem++;
                    if (currentItem < itemList.Length)
                        itemList[currentItem].GetComponent<Outline>().eraseRenderer = false;
                }
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
        if (item.tag == "ToiletPaper")
            Destroy(item.transform.parent.gameObject);
        else
            Destroy(item);

        ShowPrompt(false);
    }

    void ShowPrompt(bool show, string itemName = "")
    {
        if (show && itemName == "Cashier")
        {
            pickupPrompt.text = $"[F] Checkout";

        }
        else if (show)
        {   
            pickupPrompt.text = $"[F] Pick Up {itemName}";
            // Debug.Log($"[F] to pick up {itemName}");
        }
        else
        {
            Debug.Log(" ");
        }

        pickupPrompt.gameObject.SetActive(show);
    }
}
