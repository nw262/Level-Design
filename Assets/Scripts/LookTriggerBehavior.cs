using UnityEngine;

public class LookTriggerBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("Collided with: " + collider);
        if (collider.CompareTag("Player"))
        {
            // Debug.Log(HeadTurn.ShouldLook);
            if (HeadTurn.ShouldLook)
            {
                HeadTurn.ShouldLook = false;
            }
            else
            {
                HeadTurn.ShouldLook = true;
            }
        }
    }
}
