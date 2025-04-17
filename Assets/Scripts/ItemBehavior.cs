using UnityEngine;

public class ItemBehavior : MonoBehaviour
{   
    public GameObject crossout; // the image that makes the item crossed out on the shopping list
    public Hallucination hallucinationManager;
    public HallucinationType type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (type != HallucinationType.None)
            hallucinationManager.TriggerHallucination(type);
            
        crossout.SetActive(true);
    }
}
