using UnityEngine;

public class ChildCutscene : MonoBehaviour
{
    public Transform destination;
    public float speed = 2f;

    void Update()
    {
        // Move towards the destination at a steady speed
        transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);

        // Stop moving and destroy the object after reaching the destination
        if (Vector3.Distance(transform.position, destination.position) < 0.1f)
        {
            transform.position = destination.position; 
            enabled = false;
            Destroy(gameObject); 
        }
    }
}
