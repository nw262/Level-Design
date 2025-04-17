using UnityEngine;

public class ItemBehavior : MonoBehaviour
{   
    public GameObject crossout; // the image that makes the item crossed out on the shopping list
    public Hallucination hallucinationManager;
    public HallucinationType type;

    void OnDestroy()
    {   
        if (!Application.IsPlaying(gameObject) || gameObject == null)
            return;

        // Safely skip if required references are null
        if (hallucinationManager == null || crossout == null)
            return;

        if (type != HallucinationType.None)
            hallucinationManager.TriggerHallucination(type);

        crossout.SetActive(true);
    }
}
